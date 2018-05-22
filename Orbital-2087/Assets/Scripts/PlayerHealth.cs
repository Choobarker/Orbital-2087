using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour 
{
	private const float STARTING_HEALTH = 100;

    private bool shieldActive = false;

    private float health;
    private float maxHealth;
    private float shieldDurationLeft = 0;

    public Transform Basic;
    public Transform explosion;
	public Transform hitSplash;

    private SpriteRenderer spriteRenderer;
    public Sprite ship;
    public Sprite shipShielded;

    private DisplayPlayerHealth healthDisplay;
    private ShootProjectile playerWeapon;
    private BoostTimerController btc;

    public Slider healthbar;

    void Start()
    {
        health = STARTING_HEALTH;
        maxHealth = STARTING_HEALTH;
        healthDisplay = GameObject.FindGameObjectWithTag("PlayerHealthDisplay").GetComponent<DisplayPlayerHealth>();
        playerWeapon = gameObject.GetComponent<ShootProjectile>();
        btc = gameObject.GetComponent<BoostTimerController>();
        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
        healthbar.enabled = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("Max Health: " + maxHealth);
    }

    void Update()
    {
        if(shieldActive)
        {
            shieldDurationLeft -= Time.deltaTime;

            if(shieldDurationLeft <= 0)
            {
                shieldActive = false;
                spriteRenderer.sprite = ship;
            }

            btc.UpdateShieldTimer(shieldDurationLeft);
        }
    }

    public float CalculateHealth()
    {
        return healthbar.value = health / maxHealth;
    }

    public void ActivateShield(float duration)
    {
        if(!shieldActive)
        {
            shieldActive = true;
            shieldDurationLeft = duration + Time.deltaTime;
            spriteRenderer.sprite = shipShielded;
        }
        else
        {
            shieldDurationLeft += duration;
        }

        btc.SetShieldTimer(shieldDurationLeft);
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
        //health = maxHealth;
        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
    }

    public void HealPlayer(float amount)
    {
        if (health + amount < maxHealth)
        {
            health += amount;
        }
        else
        {
            health = maxHealth;
        }
        healthDisplay.UpdateText(health);
        healthbar.value = CalculateHealth();
    }

    public void PlayerHit(float damage)
    {
        if(!shieldActive)
        {
            TakeDamage(damage);

            if(!CheckHealth())
            {
                DestroyPlayer();
                StartCoroutine("Waiting");
            }
        }
    }

    void TakeDamage(float damage)
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

    void DestroyPlayer()
    {
        playerWeapon.setWeaponActive(false);
        Destroy(Instantiate(explosion, transform.position, transform.rotation).gameObject, 2);
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Waiting()
	{
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(this.gameObject);        
    }
}
