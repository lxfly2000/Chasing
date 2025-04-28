using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuCommands : MonoBehaviour
{
    public GameObject optionMenuObject;
    public GameObject characterMenuObject;
    public GameObject keyConfigMenuObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMenuAction(int valueIndex)
    {
        characterMenuObject.GetComponent<MenuContainer>().OpenMenuContainer(true);
    }

    public void OptionMenuAction(int valueIndex)
    {
        optionMenuObject.GetComponent<MenuContainer>().OpenMenuContainer(false);
    }

    public void ExitMenuAction(int valueIndex)
    {
        Application.Quit();
        print("Application Quit");
    }

    public void FullscreenMenuAction(int valueIndex)
    {
        GameManager.gameSettings.Fullscreen = valueIndex == 1;
        Screen.SetResolution(GameManager.internalWidth, GameManager.internalHeight, GameManager.gameSettings.Fullscreen);
    }

    public void OptionsMenuLoad(GameObject[]menuItems)
    {
        menuItems[0].GetComponent<MenuItem>().CurrentValueIndex = GameManager.gameSettings.Fullscreen ? 1 : 0;
    }

    public void OnKeyConfigAction(int valueIndex)
    {
        keyConfigMenuObject.GetComponent<MenuContainer>().OpenMenuContainer(false);
    }
}
