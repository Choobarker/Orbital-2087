using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public float fireRate = 1;
    public float damage = 10;    
    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public LayerMask whatToHit;
    public Transform bullet;
    public Transform earth;
    private Transform firepoint;
	
	void Update() 
    {
        if (Time.time > timeToFire)
        {
            firepoint = transform.Find("FirePoint");
            if (firepoint == null)
            {

                Debug.LogError("No firePoint!");
            }

            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
	}

    void Shoot()
    {
        Vector2 earthPosition = new Vector2(earth.position.x, earth.position.y);
        Vector2 firePointPosition = new Vector2(firepoint.position.x, firepoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, earthPosition - firePointPosition, 100, whatToHit);

        if (Time.time >= timeToSpawnEffect)
        {
            CreateBullet();
            timeToSpawnEffect = Time.time + 1/effectSpawnRate;
        }
    }

    void CreateBullet()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }

}
