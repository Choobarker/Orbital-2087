using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInterface : MonoBehaviour 
{
    PlayerHealth playerHealth;
    ShootProjectile playerWeapon;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
    }

    public float GetPlayerCurrency()
    {
        // TODO
        return 100;
    }

    public void UpgradeDamage(float increase)
    {
        // TODO
    }

    public void UpgradeFireRate(float increase)
    {
        // TODO
    }

    public void UpgradeHealth(float increase)
    {
        // TODO
    }
}
