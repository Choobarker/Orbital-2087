using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public Transform hitSplash;

    public GameObject fireRateBoost, shieldBoost, speedBoost, projectile;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
		if(collider.tag == projectile.tag)
        {
            Destroy(Instantiate(hitSplash, collider.transform.position, collider.transform.rotation).gameObject, 2);
            
            playerHealth.PlayerHit(collider.name);
        }

        Destroy(collider.gameObject);
    }

    private bool IsFireRateBoost(Collider2D collider)
    {
        return collider.name == fireRateBoost.name;
    }
}
