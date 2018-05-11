using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    private bool shield = false;

    public Transform hitSplash;
    public GameObject fireRateBoost, shieldBoost, speedBoost, projectile, player;

    private PlayerHealth playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
		if(collider.tag == projectile.tag)
        {
            Destroy(Instantiate(hitSplash, collider.transform.position, collider.transform.rotation).gameObject, 2);
            
            playerHealth.PlayerHit(collider.name);
        }
        else if(collider.name == fireRateBoost.name)
        {
            collider.GetComponent<FireRateBoost>().Activate();
        }
        else if(collider.name == shieldBoost.name)
        {
            collider.GetComponent<ShieldBoost>().Activate();
        }
        else if(collider.name == speedBoost.name)
        {
            collider.GetComponent<SpeedBoost>().Activate();
        }

        Destroy(collider.gameObject);
    }
}
