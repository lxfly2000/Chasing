using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public GameObject mainMenuContainer;
    public GameObject pressAnyKeyText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pressAnyKeyText.activeSelf)
        {
            GameManager.UpdateKeyState();
            // ‰»Î¥¶¿Ì
            if (GameManager.GetKeyDown(GameManager.AxisKeyID.Fire1))
            {
                pressAnyKeyText.SetActive(false);
                mainMenuContainer.GetComponent<MenuContainer>().OpenMenuContainer(false);
            }
        }
   }
}
