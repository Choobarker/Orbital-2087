using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour 
{
    private bool fireRateBoostActive = false;

    public float fireRate;
    private float nextFire;
    private float damage = 1;
    private float boostDurationLeft = 0;
    private float boostMultiplier = 0;    

    public GameObject projectile;
    public Transform projectileSpawn;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1 / fireRate;
            CreateShot();
        }

        if(fireRateBoostActive)
        {
            boostDurationLeft -= Time.deltaTime;

            if(boostDurationLeft <= 0)
            {
                fireRateBoostActive = false;
                fireRate /= boostMultiplier;
            }
        }
    }

    public GameObject CreateShot()
    {
        GameObject shot = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        shot.GetComponent<ProjectileInfo>().SetDamage(damage);
        return shot;
    }

    public void ActivateFireRateBoost(float duration, float boostMultiplier)
    {
        if(!fireRateBoostActive)
        {
            fireRateBoostActive = true;
            fireRate *= boostMultiplier;
            this.boostMultiplier = boostMultiplier;
            boostDurationLeft = duration + Time.deltaTime;
        }
        else
        {
            boostDurationLeft += duration;
        }
    }

    public void UpgradeDamege(float increase)
    {
        damage += increase;
    }

    public float GetDamage()
    {
        return damage;
    }
}
