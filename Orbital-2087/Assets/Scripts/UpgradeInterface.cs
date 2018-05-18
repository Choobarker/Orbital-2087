using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour 
{
    PlayerHealth playerHealth;
    ShootProjectile playerWeapon;
    public GameObject menu;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public float GetPlayerCurrency()
    {
        // TODO
        return 100;
    }

    public void HealPlayer(float health)
    {
        playerHealth.HealPlayer(health); 
    }

    public void UpgradeDamage(float increase)
    {
        playerWeapon.SetDamage(increase);
    }

    public void UpgradeFireRate(float increase)
    {
        // TODO
    }

    public void UpgradeHealth(float increase)
    {
        playerHealth.HealPlayer(increase);
    }
}
