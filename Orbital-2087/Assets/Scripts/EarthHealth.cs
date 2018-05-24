using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour 
{
    private float STARTING_HEALTH = 500;
    private float health;

    public Transform Basic;
    public Transform earthExplosion;
    public Transform hitSplash;

    private DisplayEarthHealth healthDisplay;
    public Slider healthbar;

    private void Start()
    {
        health = STARTING_HEALTH;
        healthDisplay = GameObject.FindGameObjectWithTag("EarthHealthDisplay").GetComponent<DisplayEarthHealth>();
        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
        healthbar.enabled = false;
    }

    public float CalculateHealth()
    {
        return health / STARTING_HEALTH;
    }

    public void HealEarth(float amount)
    {
        if ((health + amount) < STARTING_HEALTH)
        {
            health += amount;
        }
        else
        {
            health = STARTING_HEALTH;
        }

        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStartingHealth()
    {
        return STARTING_HEALTH;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {        
        if(collider.tag == "Projectile")
        {
            TakeDamage(collider.GetComponent<ProjectileInfo>().GetDamage());   

            if(!CheckHealth())
            {
                DestroyEarth();
                StartCoroutine("Waiting");
            }
        }
        
        Destroy(Instantiate(hitSplash, collider.transform.position, collider.transform.rotation).gameObject, 2);
        Destroy(collider.gameObject);        
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;

        if(health < 0)
        {
            health = 0;
        }

        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
    }

    bool CheckHealth()
    {
        bool isAlive = true;

        if(health <= 0)
        {
            isAlive = false;
            health = 0;
        }

        return isAlive;
    }

    void DestroyEarth()
    {
        Destroy(Instantiate(earthExplosion, new Vector3(0, 0, 0), transform.rotation).gameObject, 2);
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Waiting() 
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(this.gameObject);        
    }
}
