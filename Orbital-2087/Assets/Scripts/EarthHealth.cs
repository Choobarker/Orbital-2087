using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHealth : MonoBehaviour {

    private float health = 100;
    private float damageTaken = 10;
    public Transform Basic;


    void OnTriggerEnter2D(Collider2D Bullet)
    {
        Destroy(Bullet.gameObject);
        Debug.Log("Earth hit!");

        if (health > 0)
        {
            DamageTaken(Bullet);
        }
        else {
            Debug.Log("Earth has been DESTROYED!!!");
        } 

    }

    void DamageTaken(Collider2D weaponType) {
        
        health = health - damageTaken;
        Debug.Log("Health: "+health);
    }

}
