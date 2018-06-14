using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour 
{
    private const float STARTING_HEALTH = 1000;
    private const float HALF_HEALTH = (STARTING_HEALTH / 2);
    private const float QUARTER_HEALTH = (STARTING_HEALTH / 4);
    
    private float health;

    private bool destroyed = false;

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
        if(health <= STARTING_HEALTH && health > HALF_HEALTH)
        {
            healthFill.color = Color.green;
        }

        if(health <= HALF_HEALTH && health > QUARTER_HEALTH)
        {
            healthFill.color = Color.yellow;
        }

        if(health <= QUARTER_HEALTH)
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
        if((health + amount) < STARTING_HEALTH)
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
        if(!destroyed)
        {
            destroyed = true;
            Destroy(Instantiate(earthExplosion, new Vector3(0, 0, 0), transform.rotation).gameObject, 2);
            gameObject.GetComponent<Renderer>().enabled = false;
        }        
    }

    IEnumerator Waiting() 
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(this.gameObject);        
    }
}
