using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public BaseUnit usingUnit = null;
    public float jumpForce = 5.0f;
    public float jumpTime = 0.2f;
    public string playerName = "";
    private float jumpLeftTime = 0.0f;
    private Vector3 initialPos = Vector3.zero;
    private Quaternion initialRotate = Quaternion.identity;
    public GameObject bulletObject = null;
    public int maxBulletCount = 5;
    public int bulletCount = 0;
    private AudioClip jumpSound = null;
    private AudioClip shootSound = null;
    private GameObject floorObject = null;

    // Start is called before the first frame update
    void Start()
    {
        usingUnit = GameManager.unitData.defaultCharacterData.Find(o => o.name.Equals(playerName));
        usingUnit.equipment1 = GameManager.playerData.possessingObjects.Find(o => o.id == GameManager.playerData.equipment[0]);
        usingUnit.equipment2 = GameManager.playerData.possessingObjects.Find(o => o.id == GameManager.playerData.equipment[1]);
        jumpLeftTime = jumpTime;
        initialPos = transform.position;
        initialRotate = transform.rotation;
        bulletCount = 0;
        jumpSound = Resources.Load<AudioClip>("jump");
        shootSound = Resources.Load<AudioClip>("shoot");
        floorObject = GameObject.Find("StartPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && jumpLeftTime > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            if (jumpSound != null && jumpLeftTime == jumpTime)
                AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);
            jumpLeftTime = jumpLeftTime - Time.fixedDeltaTime;
        }
    }

    private void FixedUpdate()
    {
        transform.SetPositionAndRotation(new Vector3(initialPos.x, transform.position.y, initialPos.z), initialRotate);
        GameManager.UpdateKeyState();
        if (GameManager.GetKeyDown(GameManager.AxisKeyID.Fire1) && bulletObject != null && bulletCount < maxBulletCount)
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            GameObject b = Instantiate(bulletObject, transform.position, Quaternion.identity);
            b.GetComponent<BulletControl>().senderObject = gameObject;
        }
        if (GameManager.GetKeyDown(GameManager.AxisKeyID.Fire2))
            SceneManager.LoadScene("TitleScene");
        if(transform.position.y<floorObject.transform.position.y)
        {
            //µô³öÆÁÄ»
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("damage"), Camera.main.transform.position);
            usingUnit.health = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
            jumpLeftTime = jumpTime;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Input.GetAxis("Vertical") <= 0)
            jumpLeftTime = 0;
    }
}
