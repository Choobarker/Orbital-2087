using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour 
{
	public static float health = 100;
    private float damageTaken = 10;
    public Transform Basic;
    public Transform explosion;
	public Transform hitSplash;


    void OnTriggerEnter2D(Collider2D Bullet)
    {
		Debug.Log("taking hits");
		Destroy(Instantiate(hitSplash, Bullet.transform.position, Bullet.transform.rotation).gameObject, 2);
		DamageTaken(Bullet);
        Destroy(Bullet.gameObject);
        
        if(!CheckHealth())
        {
            DestroyPlayer();
            StartCoroutine("Waiting");
        }
        
    }

    void DamageTaken(Collider2D weaponType) 
	{        
        health = health - damageTaken;
        Debug.Log("Player Health: " + health);
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
