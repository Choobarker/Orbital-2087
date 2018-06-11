using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{    
    public float fireRate = 1;
    private float damage = 5;
    private float delayBeforeFirstShot = 2.5f;
    private float nextFire = 0;
    
    public Transform projectile;
    public Transform firepoint;

    void Start()
    {
        nextFire += Time.time + delayBeforeFirstShot;
    }
	
	void Update() 
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + 1 / fireRate;
            Shoot();
        }
	}

    void Shoot()
    {
        Instantiate(projectile, firepoint.position, firepoint.rotation).GetComponent<ProjectileInfo>().SetDamage(damage);
    }

    public void BuffDamage(float buff)
    {
        damage += buff;
    }
}