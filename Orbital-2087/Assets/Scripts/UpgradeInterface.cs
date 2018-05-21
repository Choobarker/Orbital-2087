using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour 
{
    private int fireRateLevel;
    private int damageLevel;
    private int maxHealthLevel;

    private float baseFireRate = 2;
    private float baseDamage = 1;
    private float baseHealth = 100;

    private float fireRateLevelIncrease = 1.2f;
    private float damageLevelIncrease = 1.2f;
    private float maxHealthLevelIncrease = 15;

    private float levelCostMultiplier = 1.4f;

    PlayerHealth playerHealth;
    ShootProjectile playerWeapon;
    ScoreKeeping scoreKeeping;
    EarthHealth earthHealth;

    UpgradeMenu cashText, fireRateLevelText, damageLevelText, healthLevelText, fireRateCostText, damageCostText, healthCostText;
    private float fireRateUpgradeCost, damageUpgradeCost, healthUpgradeCost;
    private float healingCost = 5f; // default healing cost for the earth & player

    void Start()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
        scoreKeeping = player.GetComponent<ScoreKeeping>();
        earthHealth = earth.GetComponent<EarthHealth>();

        //initializes all the text fields on the upgrade menu
        cashText = GameObject.FindGameObjectWithTag("CashDisplay").GetComponent<UpgradeMenu>();
        fireRateCostText = GameObject.FindGameObjectWithTag("FireRatePrice").GetComponent<UpgradeMenu>();
        fireRateLevelText = GameObject.FindGameObjectWithTag("FireRateLevel").GetComponent<UpgradeMenu>();
        damageLevelText = GameObject.FindGameObjectWithTag("DamageLevel").GetComponent<UpgradeMenu>();
        damageCostText = GameObject.FindGameObjectWithTag("DamagePrice").GetComponent<UpgradeMenu>();
        healthLevelText = GameObject.FindGameObjectWithTag("MaxHealthLevel").GetComponent<UpgradeMenu>();
        healthCostText = GameObject.FindGameObjectWithTag("MaxHealthPrice").GetComponent<UpgradeMenu>();

        CalculateLevels();
    }

    void Update()
    {
        fireRateUpgradeCost = GetCost(fireRateLevel);
        damageUpgradeCost = GetCost(damageLevel);
        healthUpgradeCost = GetCost(maxHealthLevel);

        cashText.UpdateCashText(GetPlayerCurrency());

        //updates the fire rate level and price once per frame
        fireRateLevelText.UpdateFireRateLevel(fireRateLevel);
        fireRateCostText.UpdateFireRatePrice(fireRateUpgradeCost);

        //updates the damage level and the price once per frame
        damageLevelText.UpdateDamageLevel(damageLevel);
        damageCostText.UpdateDamagePrice(damageUpgradeCost);

        //updates the health level and the price once per frame
        healthLevelText.UpdateHealthLevel(maxHealthLevel);
        healthCostText.UpdateMaxHealthPrice(healthUpgradeCost);
    }

    public float GetPlayerCurrency()
    {
        return scoreKeeping.GetCash();
    }

    public void HealPlayer(float health)
    {
        if (GetPlayerCurrency() >= healingCost)
        {
            if (playerHealth.GetHealth() < GetMaxHealth())
            {
                playerHealth.HealPlayer(health);
                DeductCost(healingCost);
            }
            else
            {
                Debug.Log("You are at max health");
            }
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    public void HealEarth(float health)
    {
        if (GetPlayerCurrency() >= healingCost)
        {
            if (earthHealth.GetHealth() < 500)
            {
                earthHealth.HealEarth(health);
                DeductCost(healingCost);
            }
            else
            {
                Debug.Log("The earth is at max health");
            }
        }
        else
        {
            Debug.Log("Not enough cash to purchase this");
        }
    }

    public void UpgradeDamage()
    {
        damageUpgradeCost = GetCost(damageLevel);
        if (GetPlayerCurrency() >= damageUpgradeCost)
        {
            ++damageLevel;
            playerWeapon.SetDamage(GetDamage() + damageLevelIncrease);
            DeductCost(damageUpgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    public void UpgradeFireRate()
    {
        fireRateUpgradeCost = GetCost(fireRateLevel);
        if (GetPlayerCurrency() >= fireRateUpgradeCost)
        {
            ++fireRateLevel;
            playerWeapon.SetFireRate(GetFireRate() + fireRateLevelIncrease);
            DeductCost(fireRateUpgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");

            //error message for insufficient funds
        }
    }

    public void UpgradeHealth()
    {
        healthUpgradeCost = GetCost(maxHealthLevel);
        if (GetPlayerCurrency() >= healthUpgradeCost)
        {
            ++maxHealthLevel;
            playerHealth.SetMaxHealth(GetMaxHealth() + maxHealthLevelIncrease);
            DeductCost(healthUpgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    void CalculateLevels()
    {
        fireRateLevel = (int)((GetFireRate() - baseFireRate) / fireRateLevelIncrease);
        damageLevel = (int)((GetDamage() - baseDamage) / damageLevelIncrease);
        maxHealthLevel = (int)((GetMaxHealth() - baseHealth) / maxHealthLevelIncrease);
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

    public float GetCost(int currentLevel)
    {
        CalculateLevels();
        return (currentLevel + 1) * levelCostMultiplier;
    }

    public void DeductCost(float cost)
    {
        scoreKeeping.SetCash(scoreKeeping.GetCash() - cost);
    }
}
