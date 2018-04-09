﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour 
{
	public float speed;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVeritical = Input.GetAxis ("Vertical");
		Rigidbody rb = GetComponent<Rigidbody> ();

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVeritical);
		rb.velocity = movement * speed;
	}
}
