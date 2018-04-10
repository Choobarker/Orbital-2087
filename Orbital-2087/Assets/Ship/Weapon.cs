using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    Transform firepoint;
    
	// Use this for initialization
	void Awake () {
        firepoint = transform.Find("FirePoint");
        if (firepoint == null){

            Debug.LogError("No firePoint!");
        }
    }
	
	void Update () {

            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
	}

    void Shoot (){

        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firepoint.position.x, firepoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        if (Time.time >= timeToSpawnEffect){
            Effect();
            timeToSpawnEffect = Time.time + 1/effectSpawnRate;
        }
        
        if (hit.collider != null){

            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage." );
        }
    }

    void Effect(){

        Instantiate(BulletTrailPrefab, firepoint.position, firepoint.rotation);
    }
}
