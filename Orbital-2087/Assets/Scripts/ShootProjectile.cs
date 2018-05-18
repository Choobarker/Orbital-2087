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

    private bool weaponActive = true;

    public GameObject projectile;
    public Transform projectileSpawn;

    private BoostTimerController btc;

    void Start()
    {
        btc = gameObject.GetComponent<BoostTimerController>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && weaponActive)
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

            btc.UpdateFireRateTimer(boostDurationLeft);
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

        btc.SetFireRateTimer(boostDurationLeft);
    }

    public void SetDamage(float increase)
    {
        damage += increase;
    }

    public float GetDamage()
    {
        return damage;
    }
    
    public void setWeaponActive(bool active)
    {
        weaponActive = active;
    }
}
