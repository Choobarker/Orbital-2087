using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
	public float speed;
	public Rigidbody rb;

	void Start()
	{
		rb = (Rigidbody)GetComponent (typeof(Rigidbody));

		Vector3 movement = new Vector3 (0.0f, 0.0f, 1.0f);
		GetComponent<Rigidbody> ().velocity = movement * speed;
	}	
}
