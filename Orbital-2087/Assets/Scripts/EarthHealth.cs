using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour 
{
    private const float STARTING_HEALTH = 1000;
    private float health;

    public Transform Basic;
    public Transform earthExplosion;
    public Transform hitSplash;

    public Slider healthbar;
    public Image healthFill;
    public Text healthText;

    private void Start()
    {
        health = STARTING_HEALTH;
        UpdateHealthText();
        healthbar.value = CalculateHealth();
        healthbar.enabled = false;
    }

    public void UpdateHealthText()
    {
        healthText.text = health + "/" + STARTING_HEALTH;
    }

    public void UpdateFillColour()
    {
        float halfHealth = (STARTING_HEALTH / 2);
        float quarterHealth = (STARTING_HEALTH / 4);

        if (health <= STARTING_HEALTH && health > halfHealth)
        {
            healthFill.color = Color.green;
        }

        if (health <= halfHealth && health > quarterHealth)
        {
            healthFill.color = Color.yellow;
        }

        if (health <= quarterHealth)
        {
            healthFill.color = Color.red;
        }
    }

    public float CalculateHealth()
    {
        UpdateFillColour();
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

        UpdateHealthText();
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

        UpdateHealthText();
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
