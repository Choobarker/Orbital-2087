using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    public Text cashText;
    public Text fireRateText;
    public Text damageText;
    public Text maxHealthText;
    public Text fireRatePrice;
    public Text damagePrice;
    public Text maxHealthPrice;

    public UpgradeInterface upgradeInterface;
    public GameObject upgradeButton;

    void Start()
    {
        upgradeInterface = gameObject.GetComponent<UpgradeInterface>();
    }

    public void DisableButton()
    {
        upgradeButton.SetActive(false);
    }

    public void EnableButton()
    {
        upgradeButton.SetActive(true);
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
        
        if(upgradeInterface == null)
        {
            Start();
        }

        upgradeInterface.RefreshTexts();

        //pause the game while the upgrade menu is open
        Time.timeScale = 0f;
    }

    public void UpdateDamagePrice(float price, bool canAfford)
    {
        if (damagePrice == null)
        {
            Start();
        }

        damagePrice.text = "$" + price;
    }

    public void UpdateMaxHealthPrice(float price, bool canAfford)
    {
        if (maxHealthPrice == null)
        {
            Start();
        }

        maxHealthPrice.text = "$" + price;
    }

    public void UpdateFireRatePrice(float price, bool canAfford)
    {
        if (fireRatePrice == null)
        {
            Start();
        }

        fireRatePrice.text = "$" + price;
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
