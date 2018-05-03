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
        DestroyShots();
	}
    
    //destroys the fired shots after 4 seconds as they will have exited the game area.
    void DestroyShots()
    {
        float lifeTime = 4.0f;
        Destroy(gameObject, lifeTime);
    }

}
