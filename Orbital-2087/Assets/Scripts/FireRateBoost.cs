using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : MonoBehaviour 
{
    GameObject player;
    ShootProjectile playerWeapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = player.GetComponent<ShootProjectile>();
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");

        playerWeapon.BoostFireRate(1f, 0f);
    }
}
