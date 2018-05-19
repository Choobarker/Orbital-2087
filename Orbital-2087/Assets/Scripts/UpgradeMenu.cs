using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    private TextFade fade = new TextFade();
    public Text cashText;
    public Text fireRateText;
    public Text damageText;
    public Text maxHealthText;

    void Start()
    {
        cashText = GetComponent<Text>();
        fireRateText = GetComponent<Text>();
        maxHealthText = GetComponent<Text>();
        damageText = GetComponent<Text>();
    }

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

    //methods for updating the players stats & cash on the screen
    public void UpdateCashText(float cash)
    {
        if (cashText == null)
        {
            Start();
        }

        cashText.text = "CASH: $" + cash;
    }

    public void UpdateFireRateLevel(float level)
    {
        if (fireRateText == null)
        {
            Start();
        }

        fireRateText.text = "FIRERATE: (LEVEL " + level + ")";
    }

    public void UpdateHealthLevel(float level)
    {
        if (maxHealthText == null)
        {
            Start();
        }

        maxHealthText.text = "HEALTH: (LEVEL " + level + ")";
    }

    public void UpdateDamageLevel(float level)
    {
        if (damageText == null)
        {
            Start();
        }

        damageText.text = "DAMAGE: (LEVEL " + level + ")";
    }

}
