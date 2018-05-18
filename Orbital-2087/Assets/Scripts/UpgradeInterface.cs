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

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
        scoreKeeping = player.GetComponent<ScoreKeeping>();
        CalculateLevels();
    }

    public float GetPlayerCurrency()
    {
        return scoreKeeping.GetCash();
    }

    public void HealPlayer(float health)
    {
        playerHealth.HealPlayer(health); 
    }

    public void UpgradeDamage()
    {
        ++damageLevel;
        playerWeapon.SetDamage(GetDamage() + damageLevelIncrease);
    }

    public void UpgradeFireRate()
    {
        ++fireRateLevel;
        playerWeapon.SetFireRate(GetFireRate() + fireRateLevelIncrease);
    }

    public void UpgradeHealth()
    {
        ++maxHealthLevel;
        playerHealth.HealPlayer(GetMaxHealth() + maxHealthLevelIncrease);
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
