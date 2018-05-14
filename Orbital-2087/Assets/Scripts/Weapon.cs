using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public float fireRate = 1;
    public float damage = 10;
    private float delayBeforeFirstShot = 2.5f;
    private float nextFire = 0;

    public LayerMask whatToHit;
    public Transform projectile;
    public Transform earth;
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
        // Vector2 earthPosition = new Vector2(earth.position.x, earth.position.y);
        // Vector2 firePointPosition = new Vector2(firepoint.position.x, firepoint.position.y);
        // RaycastHit2D hit = Physics2D.Raycast(firePointPosition, earthPosition - firePointPosition, 100, whatToHit);

        Instantiate(projectile, firepoint.position, firepoint.rotation).GetComponent<ProjectileInfo>().SetDamage(damage);
    }
}
