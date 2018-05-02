using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
	public float speed;
    private Rigidbody rb;

    void Start()
	{
		rb = (Rigidbody)GetComponent (typeof(Rigidbody));
        rb.velocity = transform.up * speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        //checks if the gameobject is an ememy ship and destroys it.
        if (GameObject.Find("EnemySpaceship"))
        {
            GameObject enemy = GameObject.Find("EnemySpaceship");
            Destroy(enemy);
            Debug.Log(name + " was destroyed");
        }
    }
}
