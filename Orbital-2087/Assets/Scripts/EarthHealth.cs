using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHealth : MonoBehaviour {

    private float health = 500;
    private float damageTaken = 10;
    public Transform Basic;
    public Transform earthExplosion;


    void OnTriggerEnter2D(Collider2D Bullet)
    {
        Destroy(Bullet.gameObject);
        // Debug.Log("Earth hit!");

        /*
        if (health > 0)
        {
            DamageTaken(Bullet);
        }
        else {
            Debug.Log("Earth has been DESTROYED!!!");
        }
        */

        DamageTaken(Bullet);

        if(!CheckHealth())
        {
            DestroyEarth();
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
        }

        return isAlive;
    }

    void DestroyEarth()
    {
        Instantiate(earthExplosion, new Vector3(0, 0, 0), transform.rotation);

        Destroy(this.gameObject);
    }

}
