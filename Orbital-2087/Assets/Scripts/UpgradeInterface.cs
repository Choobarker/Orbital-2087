using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour 
{
    private int fireRateLevel;
    private int damageLevel;
    private int maxHealthLevel;

    private float upgradeCost = 5f;

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

    UpgradeMenu cashText;
    UpgradeMenu fireRateText;
    UpgradeMenu damageLevelText;
    //UpgradeMenu healthLevelText;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
        scoreKeeping = player.GetComponent<ScoreKeeping>();

        cashText = GameObject.FindGameObjectWithTag("CashDisplay").GetComponent<UpgradeMenu>();
        fireRateText = GameObject.FindGameObjectWithTag("FireRateLevel").GetComponent<UpgradeMenu>();
        damageLevelText = GameObject.FindGameObjectWithTag("DamageLevel").GetComponent<UpgradeMenu>();
        //healthLevelText = GameObject.FindGameObjectWithTag("MaxHealthLevel").GetComponent<UpgradeMenu>(); // wasn't working properly need to debug

        CalculateLevels();
    }

    void Update()
    {
        cashText.UpdateCashText(GetPlayerCurrency());
        fireRateText.UpdateFireRateLevel(fireRateLevel);
        damageLevelText.UpdateDamageLevel(damageLevel);
        //healthLevelText.UpdateHealthLevel(maxHealthLevel); // wasn't working properly need to debug
    }

    public float GetPlayerCurrency()
    {
        return scoreKeeping.GetCash();
    }

    public void HealPlayer(float health)
    {
        if (GetPlayerCurrency() >= upgradeCost)
        {
            playerHealth.HealPlayer(health);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    public void UpgradeDamage()
    {
        if (GetPlayerCurrency() >= upgradeCost)
        {
            ++damageLevel;
            playerWeapon.SetDamage(GetDamage() + damageLevelIncrease);
            DeductCost(upgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }

        CalculateLevels();
    }

    public void UpgradeFireRate()
    {
        if (GetPlayerCurrency() >= upgradeCost)
        {
            ++fireRateLevel;
            playerWeapon.SetFireRate(GetFireRate() + fireRateLevelIncrease);
            DeductCost(upgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");

            //error message for insufficient funds
        }

        CalculateLevels();
    }

    public void UpgradeHealth()
    {
        if (GetPlayerCurrency() >= upgradeCost)
        {
            ++maxHealthLevel;
            playerHealth.SetMaxHealth(GetMaxHealth() + maxHealthLevelIncrease);
            DeductCost(upgradeCost);
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }

        CalculateLevels();
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
        return (currentLevel + 1) * levelCostMultiplier;
    }

    public void DeductCost(float cost)
    {
        scoreKeeping.SetCash(scoreKeeping.GetCash() - cost);
    }
}
