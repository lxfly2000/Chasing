using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMenuActions : MonoBehaviour
{
    public GameObject equipmentSlotContainer;
    public GameObject equipmentChooseContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseCharacter(int index)
    {
        GameManager.playerData.playerUsingPresets = index;
        equipmentSlotContainer.GetComponent<MenuContainer>().OpenMenuContainer(false);
        UpdateEquipmentSlotImage();
    }

    private int currentEquipmentSlot = 0;

    public void ChooseEquipmentSlot(int index)
    {
        currentEquipmentSlot = index;
        equipmentChooseContainer.GetComponent<MenuContainer>().OpenMenuContainer(false);
    }

    public void ChooseEquipmentItem(int index)
    {
        string v = equipmentChooseContainer.GetComponentsInChildren<MenuItem>()[index].values[0];
        BaseUnit u = GameManager.playerData.possessingObjects.Find(o => o.name.Equals(v));
        if(GameManager.playerData.equipment[currentEquipmentSlot]==u.id)
        {
            GameManager.playerData.equipment[currentEquipmentSlot] = 0;
        }
        else
        {
            for(int i=0;i<GameManager.playerData.equipment.Length;i++)
            {
                if (GameManager.playerData.equipment[i] == u.id)
                    GameManager.playerData.equipment[i] = 0;
            }
            GameManager.playerData.equipment[currentEquipmentSlot] = u.id;
        }
        UpdateEquipmentSlotImage();
    }

    private void UpdateEquipmentSlotImage()
    {
        for(int i=0;i<GameManager.playerData.equipment.Length;i++)
        {
            if (GameManager.playerData.equipment[i] == 0)
            {
                equipmentSlotContainer.GetComponentsInChildren<MenuItem>()[i].transform.Find("Image").GetComponent<Image>().sprite = null;
            }
            else
            {
                BaseUnit u = GameManager.playerData.possessingObjects.Find(o => o.id == GameManager.playerData.equipment[i]);
                equipmentSlotContainer.GetComponentsInChildren<MenuItem>()[i].transform.Find("Image").GetComponent<Image>().sprite = u.iconSprite;
            }
        }
    }

    public void ConfirmCharacter(int value)
    {
        SceneManager.LoadScene("StageScene");
    }
}
