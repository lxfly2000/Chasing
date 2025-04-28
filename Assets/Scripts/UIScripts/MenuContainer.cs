using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MenuContainterDelegateEvent : UnityEvent<GameObject[]> { }

public class MenuContainer : MonoBehaviour
{
    public enum MenuDirection
    {
        Horizontal,Vertical
    }
    public MenuDirection menuDirection = MenuDirection.Vertical;
    public GameObject parentMenuObject = null;//返回到上一级菜单的对象，并非大纲视图中的父对象
    public AudioClip closeMenuClip = null;
    public AudioClip cursorMoveClip = null;
    public AudioSource audioSource = null;
    public bool isResponding = false;//是否接收操作
    public bool acceptCancel = true;
    private int _currentMenuCursor = 0;
    public int CurrentMenuCursor
    {
        get
        {
            return _currentMenuCursor;
        }
        set
        {
            _currentMenuCursor = value;
            UpdateMenuState();
        }
    }

    private GameObject[] menuItems;//存放含有UIMenuItem组件的菜单项对象
    public MenuContainterDelegateEvent menuLoadEventAction;
    void Awake()
    {
    }

    void Start()
    {
        if (!isResponding)
            gameObject.SetActive(false);
        UpdateListOfChildItems();
    }

    public void UpdateListOfChildItems()
    {
        MenuItem[] listObject = GetComponentsInChildren<MenuItem>();//会递归查找
        menuItems = new GameObject[listObject.Length];
        for (int i = 0; i < listObject.Length; i++)
        {
            menuItems[i] = listObject[i].gameObject;
        }
    }

    private static readonly GameManager.AxisKeyID[] prevKeys = { GameManager.AxisKeyID.Left, GameManager.AxisKeyID.Up };
    private static readonly GameManager.AxisKeyID[] nextKeys = { GameManager.AxisKeyID.Right, GameManager.AxisKeyID.Down };
    private GameManager.AxisKeyID GetPreviousKey(MenuDirection _menuDirection)
    {
        return prevKeys[(int)_menuDirection];
    }

    private GameManager.AxisKeyID GetNextKey(MenuDirection _menuDirection)
    {
        return nextKeys[(int)_menuDirection];
    }

    void Update()
    {
        if (isResponding)
        {
            GameManager.UpdateKeyState();
            if (GameManager.GetKeyDown(GetPreviousKey(menuDirection)))
            {
                if (menuItems.Length > 0)
                    CurrentMenuCursor = (CurrentMenuCursor + menuItems.Length - 1) % menuItems.Length;
                if (closeMenuClip != null&&audioSource!=null)
                    audioSource.PlayOneShot(closeMenuClip);
            }
            if (GameManager.GetKeyDown(GetNextKey(menuDirection)))
            {
                if (menuItems.Length > 0)
                    CurrentMenuCursor = (CurrentMenuCursor + 1) % menuItems.Length;
                if (closeMenuClip != null && audioSource != null)
                    audioSource.PlayOneShot(closeMenuClip);
            }
            //按后退键
            if (GameManager.GetKeyDown(GameManager.AxisKeyID.Fire2) && acceptCancel)
            {
                if (parentMenuObject != null)
                {
                    parentMenuObject.GetComponent<MenuContainer>().isResponding = true;
                    parentMenuObject.SetActive(true);
                    isResponding = false;
                    gameObject.SetActive(false);
                    if (closeMenuClip != null && audioSource != null)
                        audioSource.PlayOneShot(closeMenuClip);
                }
            }
        }
    }

    void UpdateMenuState()
    {
        for(int i=0;i<menuItems.Length;i++)
        {
            menuItems[i].GetComponent<MenuItem>().Highlighted = i == CurrentMenuCursor;
        }
    }

    public void OpenMenuContainer(bool hideParentMenu)
    {
        if (parentMenuObject != null)
        {
            parentMenuObject.GetComponent<MenuContainer>().isResponding = false;
            if (hideParentMenu)
                parentMenuObject.SetActive(false);
        }
        gameObject.SetActive(true);
        isResponding = true;
        CurrentMenuCursor = 0;
        if (menuLoadEventAction != null)
            menuLoadEventAction.Invoke(menuItems);
    }
}