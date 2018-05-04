using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthHealth : MonoBehaviour {

    public static float health = 500;
    private float damageTaken = 10;
    public Transform Basic;
    public Transform earthExplosion;


    void OnTriggerEnter2D(Collider2D Bullet)
    {
        Destroy(Bullet.gameObject);
        
        DamageTaken(Bullet);

        if(!CheckHealth())
        {
            DestroyEarth();
            StartCoroutine("Waiting");
        }
        
    }

    void DamageTaken(Collider2D weaponType) {
        
        health = health - damageTaken;
        Debug.Log("Health: " + health);
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
        Instantiate(earthExplosion, new Vector3(0, 0, 0), transform.rotation);
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Waiting() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(this.gameObject);
        
    }

    

}
