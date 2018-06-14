using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour 
{
	private const float STARTING_HEALTH = 100;

    private bool shieldActive = false;
    private bool destroyed = false;

    private float health;
    private float maxHealth;
    private float shieldDurationLeft = 0;

    public Transform explosion;
	public Transform hitSplash;

    private SpriteRenderer spriteRenderer;
    public Sprite ship;
    public Sprite shipShielded;

    private PlayerWeapon playerWeapon;
    private BoostTimerController btc;

    public Slider healthbar;
    public Image healthFill;
    public Text healthText;

    void Start()
    {
        health = STARTING_HEALTH;
        maxHealth = STARTING_HEALTH;
        playerWeapon = gameObject.GetComponent<PlayerWeapon>();
        btc = gameObject.GetComponent<BoostTimerController>();

        healthbar.value = CalculateHealth();
        healthbar.enabled = false;
        UpdateHealthText();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void UpdateHealthText()
    {
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateFillColour()
    {
        float halfHealth = (maxHealth / 2);
        float quarterHealth = (maxHealth / 4);

        if (health <= maxHealth && health > halfHealth)
        {
            healthFill.color = Color.green;
        }
        
        if(health <= halfHealth && health > quarterHealth)
        {
            healthFill.color = Color.yellow;
        }
        
        if(health <= quarterHealth)
        {
            healthFill.color = Color.red;
        }
    }

    public float CalculateHealth()
    {
        UpdateFillColour();
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
        UpdateHealthText();
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
        UpdateHealthText();
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

    void DestroyPlayer()
    {
        if(!destroyed)
        {
            destroyed = true;
            playerWeapon.setWeaponActive(false);
            Destroy(Instantiate(explosion, transform.position, transform.rotation).gameObject, 2);
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }        
    }

    IEnumerator Waiting()
	{
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(this.gameObject);
    }
}
