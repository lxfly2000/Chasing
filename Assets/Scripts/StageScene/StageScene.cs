using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageScene : MonoBehaviour
{
    public float stageMovingSpeedB = 0.05f;
    public float stageMovingSpeedK = 0.02f;
    public List<GameObject> playerCharacters = new List<GameObject>();
    public List<GameObject> stageFragments = new List<GameObject>();
    public List<int> difficulties = new List<int>();
    public GameObject playerInitialPosition = null;
    public GameObject fragmentLeftPosition = null;
    public GameObject fragmentRightPosition = null;
    public StatusDisplay statusDisplay = null;
    private GameObject playerObject = null;
    public GameObject gameOverPanel = null;
    private int periodCounter = 0;
    private List<int>[] difficultyIndices = null;
    private const int maxLevel = 5;
    public StageBG stageBGObject = null;

    private List<GameObject> loadedFragments = new List<GameObject>();
    public int currentDifficulty = 1;
    private bool initOk = false;

    // Start is called before the first frame update
    void Start()
    {
        BaseUnit u = GameManager.unitData.defaultCharacterData.Find(o => o.name.Equals(GameManager.playerData.playerCharacterPresets[GameManager.playerData.playerUsingPresets].name));
        playerObject = Instantiate(playerCharacters.Find(o => o.name.Equals(u.name)), playerInitialPosition.transform.position, playerInitialPosition.transform.rotation);
        statusDisplay.playerObject = playerObject;
        stageBGObject.playerObject = playerObject;
        GameManager.playerData.playerScore = 0;

        difficultyIndices = new List<int>[maxLevel];
        for (int i = 0; i < maxLevel; i++)
            difficultyIndices[i] = new List<int>();
        for(int i=0;i<difficulties.Count;i++)
        {
            difficultyIndices[difficulties[i]].Add(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl pc = playerObject.GetComponent<PlayerControl>();
        if (!initOk)
        {
            pc.usingUnit.level = 1;
            pc.usingUnit.health = pc.usingUnit.maxHealthB + pc.usingUnit.level * pc.usingUnit.maxHealthK;
            if (pc.usingUnit.health > 0)
                initOk = true;
            return;
        }
        if (!gameOverPanel.activeSelf)
        {
            //生成右侧
            float rightPos = fragmentLeftPosition.transform.position.x;
            if (loadedFragments.Count > 0)
                rightPos = loadedFragments[loadedFragments.Count - 1].transform.Find("EndIndicator").position.x;
            if (rightPos < fragmentRightPosition.transform.position.x)
            {
                //生成不高于该等级的难度关卡
                int chooseDifficulty = Random.Range(0, pc.usingUnit.level);
                int fragmentIndex = difficultyIndices[chooseDifficulty][Random.Range(0, difficultyIndices[chooseDifficulty].Count)];
                GameObject fragment = Instantiate(stageFragments[fragmentIndex]);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                //按概率保留敌人，等级越高敌人越多
                foreach (var e in enemies)
                {
                    if (e.transform.parent.gameObject == fragment)
                    {
                        if (Random.Range(0, maxLevel) >= pc.usingUnit.level)
                        {
                            Destroy(e);
                        }
                        else
                        {
                            foreach (var o in e.GetComponentsInChildren<NonPlayerControl>())
                            {
                                o.unitData.level = pc.usingUnit.level;
                            }
                        }
                    }
                }
                //按概率保留金币，等级越高金币越少
                GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
                foreach (var e in coins)
                {
                    if (e.transform.parent.gameObject == fragment)
                    {
                        if (Random.Range(0, maxLevel) < pc.usingUnit.level)
                        {
                            Destroy(e);
                        }
                        else
                        {
                            foreach (var o in e.GetComponentsInChildren<NonPlayerControl>())
                            {
                                o.unitData.level = pc.usingUnit.level;
                            }
                        }
                    }
                }
                GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
                foreach (var e in spikes)
                {
                    if (e.transform.parent.gameObject == fragment)
                    {
                        if (Random.Range(0, maxLevel) >= pc.usingUnit.level)
                        {
                            Destroy(e);
                        }
                        else
                        {
                            foreach (var o in e.GetComponentsInChildren<NonPlayerControl>())
                            {
                                o.unitData.level = pc.usingUnit.level;
                            }
                        }
                    }
                }
                Vector3 dv = new Vector3(rightPos, fragmentRightPosition.transform.position.y, fragmentRightPosition.transform.position.z) - fragment.transform.Find("StartIndicator").position;
                fragment.transform.Translate(dv);
                loadedFragments.Add(fragment);
            }
            //消除左侧
            for (int i = 0; i < loadedFragments.Count;)
            {
                GameObject f = loadedFragments[i];
                if (f.transform.Find("EndIndicator").position.x < fragmentLeftPosition.transform.position.x)
                {
                    loadedFragments.Remove(f);
                    Destroy(f);
                }
                else
                {
                    i++;
                }
            }
            //移动
            for (int i = 0; i < loadedFragments.Count; i++)
            {
                loadedFragments[i].transform.Translate(new Vector3(-stageMovingSpeedB - stageMovingSpeedK * pc.usingUnit.level, 0, 0));
            }
            periodCounter++;
            if (periodCounter >= 50)
            {
                GameManager.playerData.playerScore += 1;
                periodCounter = 0;
            }
            if (pc.usingUnit.level < maxLevel)
            {
                if (GameManager.playerData.playerScore > pc.usingUnit.level * pc.usingUnit.level * 20)
                    pc.usingUnit.level++;
            }
            if (pc.usingUnit.health <= 0)
            {
                pc.usingUnit.health = 0;
                MenuContainer mc = gameOverPanel.GetComponent<MenuContainer>();
                mc.OpenMenuContainer(false);
                mc.transform.Find("TextScore").GetComponent<TMP_Text>().text = string.Format("Score: {0}", GameManager.playerData.playerScore);
                gameOverPanel.SetActive(true);
                playerObject.GetComponent<PlayerControl>().enabled = false;
                playerObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                playerObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void OnMenuActionRetry(int index)
    {
        SceneManager.LoadScene("StageScene");
    }

    public void OnMenuActionReturn(int index)
    {
        SceneManager.LoadScene("TitleScene");
    }
}
