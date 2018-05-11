using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour 
{
	public const float STARTING_HEALTH = 100;

    private float health;
    private float damageTaken = 10;

    private bool shieldActive = false;

    public Transform Basic;
    public Transform explosion;
	public Transform hitSplash;

    private DisplayPlayerHealth healthDisplay;

    void Start()
    {
        health = STARTING_HEALTH;
        healthDisplay = GameObject.FindGameObjectWithTag("PlayerHealthDisplay").GetComponent<DisplayPlayerHealth>();
        healthDisplay.UpdateText(health);
    }

    //adds a specified amount of health to the player
    public void HealPlayer(float amount)
    {
        health += amount;

        //ensures that the players health cannot exceed 100
        if (health >= 100)
        {
            health = STARTING_HEALTH;
        }
    }

    //just for testing that healing is working and also displaying
    //private void OnMouseDown()
    //{
    //    HealPlayer(10f);
    //}

    void OnTriggerEnter2D(Collider2D Bullet)
    {
		       
    }

    public void PlayerHit(string weaponName)
    {
        if(!shieldActive)
        {
            TakeDamage(weaponName);

            if(!CheckHealth())
            {
                DestroyPlayer();
                StartCoroutine("Waiting");
            }
        }
    }

    void TakeDamage(string weaponName)
	{
        health -= damageTaken;

        if(health < 0)
        {
            health = 0;
        }

        healthDisplay.UpdateText(health);
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
