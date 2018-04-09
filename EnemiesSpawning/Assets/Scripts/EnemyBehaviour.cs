using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform bulletProjectile;
	
	public float health = 2f;
	public float speed;

	private bool alive = false;
	private bool inPosition = false;

	public float moveTime;
	private float timeMoved = 0f;

	private Vector3 direction;
	
	void Start()
	{
		alive = true;

		direction = -transform.position / 5;
		moveTime = Random.value * 2 + 1;
	}
	
	void Update ()
	{
		if(alive && !inPosition)
		{
			transform.position += direction * Time.deltaTime * speed;
			timeMoved += Time.deltaTime;

			if(timeMoved >= moveTime)
			{
				inPosition = true;
			}
		}
	}

	void OnMouseDown()
	{
		TakeDamage(2f);
	}

	void TakeDamage(float damage)
	{
		health -= damage;

		CheckHealth();
	}

	void CheckHealth()
	{
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
		TakeDamage(1f);
	}
}
