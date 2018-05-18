using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;

    public void CloseMenu()
    {
        menu.SetActive(false);

        //resume gameplay after the upgrade menu has been closed        
        Time.timeScale = 1.0f;
    }

    public void OpenMenu()
    {
        menu.SetActive(true);

        //pause the game while the upgrade menu is open
        Time.timeScale = 0f;
    }

}
