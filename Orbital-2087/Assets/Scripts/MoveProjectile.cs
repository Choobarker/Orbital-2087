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
}
