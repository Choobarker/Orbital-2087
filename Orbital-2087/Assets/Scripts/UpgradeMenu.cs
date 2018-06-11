using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    
    public GameObject upgradeButton;
    
    public Text cashText;
    public Text fireRateText;
    public Text damageText;
    public Text maxHealthText;
    public Text fireRatePrice;
    public Text damagePrice;
    public Text maxHealthPrice;
    public Text currentHealthText;

    public Image fireRateButton, damageButton, maxHealthButton, healPlayerButton, healEarthButton;
    
    private UpgradeInterface upgradeInterface;

    void Start()
    {
        upgradeInterface = gameObject.GetComponent<UpgradeInterface>();
        upgradeInterface.ToggleUpgradeButtons();
    }

    public void DisableUpgradeButton()
    {
        upgradeButton.SetActive(false);
    }

    public void EnableUpgradeButton()
    {
        upgradeButton.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        DisableUpgradeButton();      
        Time.timeScale = 1.0f;      
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        upgradeInterface.RefreshTexts();
        Time.timeScale = 0f;
    }

    public void UpdateDamagePrice(float price, bool canAfford)
    {
        damagePrice.text = "$" + price;
    }

    public void UpdateMaxHealthPrice(float price, bool canAfford)
    {
        maxHealthPrice.text = "$" + price;
    }

    public void UpdateFireRatePrice(float price)
    {
        fireRatePrice.text = "$" + price;
    }

    public void ToggleFireRateButton(bool canAfford)
    {
        if (canAfford)
        {
            fireRateButton.color = Color.green;
        }
        else if (!canAfford)
        {
            fireRateButton.color = Color.red;
        }
    }

    public void ToggleDamageButton(bool canAfford)
    {
        if (canAfford)
        {
            damageButton.color = Color.green;
        }
        else if (!canAfford)
        {
            damageButton.color = Color.red;
        }
    }

    public void ToggleHealthButton(bool canAfford)
    {
        if (canAfford)
        {
            maxHealthButton.color = Color.green;
        }
        else if (!canAfford)
        {
            maxHealthButton.color = Color.red;
        }
    }

    public void ToggleHealingButtons(bool canAfford)
    {
        if (canAfford)
        {
            healPlayerButton.color = Color.green;
            healEarthButton.color = Color.green;
        }
        else if (!canAfford)
        {
            healPlayerButton.color = Color.red;
            healEarthButton.color = Color.red;
        }
    }
    
    public void UpdateCashText(float cash)
    {
        cashText.text = "CASH: $" + cash;
    }

    public void UpdateHealthText(float player, float playerMax, float earth, float earthMax)
    {
        currentHealthText.text = "PLAYER: " + player + "/" + playerMax + " HP\n\nEARTH: " + earth + "/" + earthMax + " HP";
    }
    
    public void UpdateFireRateLevel(float level, float fireRate)
    {
        fireRateText.text = "FIRERATE: (LEVEL " + level + ") - " + fireRate + " shots per second";
    }
    
    public void UpdateHealthLevel(float level, float health)
    {
        maxHealthText.text = "HEALTH: (LEVEL " + level + ") - " + health + " HP";
    }

    public void UpdateDamageLevel(float level, float damage)
    {
        damageText.text = "DAMAGE: (LEVEL " + level + ") - " + damage + " damage";
    }
}