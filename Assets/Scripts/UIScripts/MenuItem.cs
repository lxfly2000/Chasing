using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class MenuItemDelegateEvent : UnityEvent<int> { }

public class MenuItem : MonoBehaviour
{
    public AudioClip chooseMenuClip = null;
    public AudioSource audioSource = null;
    private bool _highlighted = false;
    public bool Highlighted//当前菜单是否被选中
    {
        get
        {
            return _highlighted;
        }
        set
        {
            _highlighted = value;
            UpdateMenuItem();
        }
    }
    public Color focusedBackgroundColor, unfocusedBackgroundColor, focusedTextColor, unfocusedTextColor;
    public GameObject textObject, leftArrowObject, rightArrowObject;
    public MenuItemDelegateEvent menuItemAction;//调用参数
    public string[] values;

    private int _currentValueIndex = 0;
    public int CurrentValueIndex
    {
        get
        {
            return _currentValueIndex;
        }
        set
        {
            _currentValueIndex = value;
            UpdateMenuItem();
        }
    }

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MenuContainer menuContainer = GetComponentInParent<MenuContainer>();//会递归查找第一个匹配的
        if (menuContainer != null)
        {
            if (menuContainer != null && menuContainer.isResponding && Highlighted)
            {
                GameManager.UpdateKeyState();
                if (GameManager.GetKeyDown(GameManager.AxisKeyID.Left) && values.Length > 1)
                {
                    CurrentValueIndex = (CurrentValueIndex + values.Length - 1) % values.Length;
                    UpdateMenuItem();
                    DoMenuItemAction();
                }
                if (GameManager.GetKeyDown(GameManager.AxisKeyID.Right) && values.Length > 1)
                {
                    CurrentValueIndex = (CurrentValueIndex + 1) % values.Length;
                    UpdateMenuItem();
                    DoMenuItemAction();
                }
                if (GameManager.GetKeyDown(GameManager.AxisKeyID.Fire1))
                {
                    DoMenuItemAction();
                }
            }
        }
    }

    public void UpdateMenuItem()
    {
        GetComponent<Image>().color = Highlighted ? focusedBackgroundColor : unfocusedBackgroundColor;
        if (textObject != null)
        {
            textObject.GetComponent<TextMeshProUGUI>().color = Highlighted ? focusedTextColor : unfocusedTextColor;
            if (values.Length > 0)
                textObject.GetComponent<TextMeshProUGUI>().text = values[CurrentValueIndex];
        }
        leftArrowObject.SetActive(Highlighted && values.Length > 1);
        rightArrowObject.SetActive(Highlighted && values.Length > 1);
    }

    public void DoMenuItemAction()
    {
        if (chooseMenuClip != null && audioSource != null)
            audioSource.PlayOneShot(chooseMenuClip);
        if (menuItemAction != null)
            menuItemAction.Invoke(CurrentValueIndex);
    }
}
