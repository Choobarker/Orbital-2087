using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour 
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float fireRate;
    private float nextFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CreateShot();
        }
    }

    public GameObject CreateShot()
    {
        GameObject shot = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation) as GameObject;
        return shot;
    }
}
