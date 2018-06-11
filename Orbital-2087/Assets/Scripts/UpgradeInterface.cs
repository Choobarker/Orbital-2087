using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour 
{
    private int fireRateLevel = 0;
    private int damageLevel = 0;
    private int maxHealthLevel = 0;

    private float fireRateLevelIncrease = .15f;
    private float damageLevelIncrease = .6f;
    private float maxHealthLevelIncrease = 15;

    private float levelCostMultiplier = 2;
    private float healingCost = 5; // default healing cost for the earth & player
    private float fireRateUpgradeCost, damageUpgradeCost, healthUpgradeCost;

    PlayerHealth playerHealth;
    PlayerWeapon playerWeapon;
    ScoreKeeping scoreKeeping;
    EarthHealth earthHealth;

    UpgradeMenu upgradeMenu;    

    void Start()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<PlayerWeapon>();
        scoreKeeping = player.GetComponent<ScoreKeeping>();
        earthHealth = earth.GetComponent<EarthHealth>();
        upgradeMenu = gameObject.GetComponent<UpgradeMenu>();

        if(upgradeMenu != null)
        {
            RefreshTexts();
            ToggleUpgradeButtons();
        }        
    }

    public float GetPlayerCurrency()
    {
        if(scoreKeeping == null)
        {
            Start();
        }

        return scoreKeeping.GetCash();
    }

    public void HealPlayer(float health)
    {
        if(CanAfford(healingCost))
        {
            if(playerHealth.GetHealth() < GetMaxHealth())
            {
                playerHealth.HealPlayer(health);
                DeductCost(healingCost);
                UpdateCashText();
            }
        }

        ToggleUpgradeButtons();
        UpdateCurrentHealthText();
    }

    public void HealEarth(float health)
    {
        if(CanAfford(healingCost))
        {
            if(earthHealth.GetHealth() < earthHealth.GetStartingHealth())
            {
                earthHealth.HealEarth(health);
                DeductCost(healingCost);
                UpdateCashText();
            }
        }

        ToggleUpgradeButtons();
        UpdateCurrentHealthText();
    }

    public void UpgradeDamage()
    {
        damageUpgradeCost = GetNextLevelCost(damageLevel);
        if(CanAfford(damageUpgradeCost))
        {
            ++damageLevel;
            playerWeapon.SetDamage(GetDamage() + damageLevelIncrease);
            DeductCost(damageUpgradeCost);
            UpdateDamageText();
            UpdateCashText();
        }
        
        ToggleUpgradeButtons();
    }

    public void UpgradeFireRate()
    {
        fireRateUpgradeCost = GetNextLevelCost(fireRateLevel);
        if (CanAfford(fireRateUpgradeCost))
        {
            ++fireRateLevel;
            playerWeapon.SetFireRate(GetFireRate() + fireRateLevelIncrease);
            DeductCost(fireRateUpgradeCost);
            UpdateFireRateText();
            UpdateCashText();
        }
        
        ToggleUpgradeButtons();
    }

    public void UpgradeHealth()
    {
        healthUpgradeCost = GetNextLevelCost(maxHealthLevel);
        if(CanAfford(healthUpgradeCost))
        {
            ++maxHealthLevel;
            playerHealth.SetMaxHealth(GetMaxHealth() + maxHealthLevelIncrease);
            DeductCost(healthUpgradeCost);
            UpdateHealText();
            UpdateCashText();
        }
        
        ToggleUpgradeButtons();
    }

    private bool CanAfford(float cost)
    {
        bool canAfford = true;

        if(GetPlayerCurrency() < cost)
        {
            canAfford = false;
        }

        return canAfford;
    }
    
    void UpdateHealText()
    {
        healthUpgradeCost = GetNextLevelCost(maxHealthLevel);
        upgradeMenu.UpdateMaxHealthPrice(healthUpgradeCost, CanAfford(healthUpgradeCost));
        upgradeMenu.UpdateHealthLevel(maxHealthLevel, GetMaxHealth());
        UpdateCurrentHealthText();
    }

    void UpdateDamageText()
    {
        damageUpgradeCost = GetNextLevelCost(damageLevel);
        upgradeMenu.UpdateDamagePrice(damageUpgradeCost, CanAfford(damageUpgradeCost));
        upgradeMenu.UpdateDamageLevel(damageLevel, GetDamage());
    }

    void UpdateFireRateText()
    {
        fireRateUpgradeCost = GetNextLevelCost(fireRateLevel);
        upgradeMenu.UpdateFireRatePrice(fireRateUpgradeCost);
        upgradeMenu.UpdateFireRateLevel(fireRateLevel, GetFireRate());
    }

    void UpdateCurrentHealthText()
    {
        float playerCurHealth = playerHealth.GetHealth();
        float playerMaxHealth = playerHealth.GetMaxHealth();
        float earthCurHealth = earthHealth.GetHealth();
        float earthMaxHealth = earthHealth.GetStartingHealth();

        upgradeMenu.UpdateHealthText(playerCurHealth, playerMaxHealth, earthCurHealth, earthMaxHealth);
    }

    public void RefreshTexts()
    {
        UpdateCashText();
        UpdateDamageText();
        UpdateFireRateText();
        UpdateHealText();
        UpdateCurrentHealthText();
    }

    public void ToggleUpgradeButtons()
    {
        if(upgradeMenu == null)
        {
            Start();
        }
        
        upgradeMenu.ToggleDamageButton(CanAfford(damageUpgradeCost));
        upgradeMenu.ToggleFireRateButton(CanAfford(fireRateUpgradeCost));
        upgradeMenu.ToggleHealthButton(CanAfford(healthUpgradeCost));
        upgradeMenu.ToggleHealingButtons(CanAfford(healingCost));
    }

    void UpdateCashText()
    {
        upgradeMenu.UpdateCashText(GetPlayerCurrency());
    }

    public float GetFireRate()
    {
        return playerWeapon.GetFireRate();
    }

    public float GetDamage()
    {
        return playerWeapon.GetDamage();
    }

    public float GetMaxHealth()
    {
        return playerHealth.GetMaxHealth();
    }

    public float GetNextLevelCost(int currentLevel)
    {
        return (currentLevel + 1) * levelCostMultiplier;
    }

    public void DeductCost(float cost)
    {
        scoreKeeping.SetCash(scoreKeeping.GetCash() - cost);
    }
}