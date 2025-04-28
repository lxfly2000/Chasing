using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public const int internalWidth = 1280;
    public const int internalHeight = 720;
    private static float lastVerticalAxis = 0;
    private static float lastHorizontalAxis = 0;
    private static float lastFire1Axis = 0;
    private static float lastFire2Axis = 0;
    private static float verticalAxis = 0;
    private static float horizontalAxis = 0;
    private static float fire1Axis = 0;
    private static float fire2Axis = 0;

    public static UnitData unitData = null;
    public class PlayerData
    {
        public List<BaseUnit> possessingObjects = new List<BaseUnit>();
        public List<BaseUnit> playerCharacterPresets = new List<BaseUnit>();
        public int playerUsingPresets = 0;
        public int playerCoin = 0;
        /// <summary>
        /// 装备ID
        /// </summary>
        public int[] equipment = { 0, 0 };
        public int playerScore = 0;
    }

    public static PlayerData playerData = null;

    public struct NameSprite
    {
        public string name;
        public Sprite sprite;
    }

    public List<string> spriteListName = null;
    public List<Sprite> spriteListSprite = null;

    public enum AxisKeyID
    {
        Up, Down, Left, Right, Fire1, Fire2
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(internalWidth, internalHeight, gameSettings.Fullscreen);

        //初始化角色数据
        if (unitData == null)
            unitData = new UnitData();
        if (playerData == null)
        {
            playerData = new PlayerData();
            foreach (var u in unitData.defaultCharacterData)
            {
                int index = spriteListName.FindIndex(o => o.Equals(u.name));
                if (index != -1)
                {
                    u.iconSprite = spriteListSprite[index];
                }
                playerData.possessingObjects.Add(u);
                playerData.playerCharacterPresets.Add(u);
            }
            foreach (var u in unitData.defaultEquipmentData)
            {
                int index = spriteListName.FindIndex(o => o.Equals(u.name));
                if (index != -1)
                {
                    u.iconSprite = spriteListSprite[index];
                }
                playerData.possessingObjects.Add(u);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateKeyState()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        fire1Axis = Input.GetAxis("Fire1");
        fire2Axis = Input.GetAxis("Fire2");
    }

    private void LateUpdate()
    {
        lastVerticalAxis = verticalAxis;
        lastHorizontalAxis = horizontalAxis;
        lastFire1Axis = fire1Axis;
        lastFire2Axis = fire2Axis;
    }

    public static bool GetKeyDown(AxisKeyID keyId)
    {
        switch (keyId)
        {
            case AxisKeyID.Up: return verticalAxis > 0 && lastVerticalAxis == 0;
            case AxisKeyID.Down: return verticalAxis < 0 && lastVerticalAxis == 0;
            case AxisKeyID.Left: return horizontalAxis < 0 && lastHorizontalAxis == 0;
            case AxisKeyID.Right: return horizontalAxis > 0 && lastHorizontalAxis == 0;
            case AxisKeyID.Fire1: return fire1Axis > 0 && lastFire1Axis == 0;
            case AxisKeyID.Fire2: return fire2Axis > 0 && lastFire2Axis == 0;
        }
        return false;
    }


    public class GameSettings
    {
        public GameSettings()
        {
            LoadSettings();
        }
        private bool _fullscreen = false;
        public bool Fullscreen
        {
            get
            {
                return _fullscreen;
            }
            set
            {
                _fullscreen = value;
                SaveSettings();
            }
        }
        private string fileName = "settings.txt";
        private void LoadSettings()
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
                Fullscreen = sr.ReadLine().CompareTo("Fullscreen") == 0;
                sr.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
        private void SaveSettings()
        {
            try
            {
                StreamWriter sr = new StreamWriter(fileName, false);//指定为False表示覆盖原有内容
                sr.WriteLine(Fullscreen ? "Fullscreen" : "Windowed");
                sr.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public static GameSettings gameSettings = new GameSettings();
}
