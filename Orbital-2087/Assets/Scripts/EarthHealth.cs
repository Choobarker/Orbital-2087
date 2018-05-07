using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour 
{
    private float startingHealth = 500;
    private float health;
    private float damageTaken = 10;

    public Transform Basic;
    public Transform earthExplosion;
    public Transform hitSplash;

    private void Start()
    {
        health = startingHealth;
    }

    void OnTriggerEnter2D(Collider2D Bullet)
    {        
        Destroy(Instantiate(hitSplash, Bullet.transform.position, Bullet.transform.rotation).gameObject, 2);
        Destroy(Bullet.gameObject);
        
        DamageTaken(Bullet);   

        if(!CheckHealth())
        {
            DestroyEarth();
            StartCoroutine("Waiting");
        }        
    }

    void DamageTaken(Collider2D weaponType) 
    {        
        health = health - damageTaken;
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
