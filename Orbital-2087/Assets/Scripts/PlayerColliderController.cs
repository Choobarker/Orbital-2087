using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
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
        AudioSource audio = collider.GetComponent<AudioSource>();

		if(collider.tag == projectile.tag)
        {
            Destroy(Instantiate(hitSplash, collider.transform.position, collider.transform.rotation).gameObject, 2);
            
            playerHealth.PlayerHit(collider.GetComponent<ProjectileInfo>().GetDamage());
        }
        else if(collider.tag == fireRateBoost.tag)
        {
            collider.GetComponent<FireRateBoost>().Activate();
            audio.Play();
        }
        else if(collider.tag == shieldBoost.tag)
        {
            collider.GetComponent<ShieldBoost>().Activate();
            audio.Play();
        }
        else if(collider.tag == speedBoost.tag)
        {
            collider.GetComponent<SpeedBoost>().Activate();
            audio.Play();
        }

        if(audio != null)
        {
            collider.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collider.gameObject, 5);
        }
        else
        {
            Destroy(collider.gameObject);
        }        
    }
}