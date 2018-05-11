using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : MonoBehaviour 
{
    private float duration = 5f;
    GameObject player;
    ShootProjectile playerWeapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = player.GetComponent<ShootProjectile>();
    }

    void OnMouseDown()
    {
        playerWeapon.BoostFireRate(1f, duration);
    }
}
