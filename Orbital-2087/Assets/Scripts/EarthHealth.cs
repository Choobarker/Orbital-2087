using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour 
{
    private float STARTING_HEALTH = 500;
    private float health;
    public float damageTaken = 10;

    public Transform Basic;
    public Transform earthExplosion;
    public Transform hitSplash;

    private DisplayEarthHealth healthDisplay;

    private void Start()
    {
        health = STARTING_HEALTH;
        healthDisplay = GameObject.FindGameObjectWithTag("EarthHealthDisplay").GetComponent<DisplayEarthHealth>();
        healthDisplay.UpdateText(health);
    }

    public float GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D Bullet)
    {        
        Destroy(Instantiate(hitSplash, Bullet.transform.position, Bullet.transform.rotation).gameObject, 2);
        Destroy(Bullet.gameObject);
        
        TakeDamage(Bullet);   

        if(!CheckHealth())
        {
            DestroyEarth();
            StartCoroutine("Waiting");
        }        
    }

    public void TakeDamage(Collider2D weaponType) 
    {
        health -= damageTaken;
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
