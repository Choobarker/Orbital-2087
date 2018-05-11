using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : MonoBehaviour 
{
    private const float DURATION = 10f;
    private const float MULTIPLIER = 2;

    ShootProjectile playerWeapon;

    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootProjectile>();
    }

    public void Activate()
    {
        playerWeapon.ActivateFireRateBoost(DURATION, MULTIPLIER);
    }
}
