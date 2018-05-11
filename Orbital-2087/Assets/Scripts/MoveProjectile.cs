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
        rb.velocity = transform.up * speed; // move the shot in a straight line
        DestroyShots(4f); // destroys shots after 4 seconds of being fired
	}

    //destroys the fired shots after a specified lifetime
    void DestroyShots(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}
