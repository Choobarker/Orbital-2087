using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerShot : MonoBehaviour
{
	private const float LIFE_TIME = 4.0f;
	public float speed;    
    private Rigidbody rb;
    
    void Start()
	{
		rb = (Rigidbody)GetComponent (typeof(Rigidbody));      
	}
    
    void FixedUpdate()
    {
    	rb.velocity = transform.up * speed;
        DestroyShots(LIFE_TIME); // destroys shots after 4 seconds of being fired
    }

    //destroys the fired shots after a specified lifetime
    void DestroyShots(float lifeTime)
    {
        Destroy(gameObject, lifeTime);
    }
}