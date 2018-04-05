using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 2f;
	public float speed;

	private bool alive = false;
	private bool inPosition = false;

	public float moveTime = 2f;
	private float timeMoved = 0f;
	
	void Start()
	{
		alive = true;
	}
	
	void Update () 
	{
		if(alive && !inPosition)
		{
			transform.position += Vector3.down * Time.deltaTime * speed;
			timeMoved += Time.deltaTime;

			if(timeMoved >= moveTime)
			{
				inPosition = true;
			}
		}
	}

	void OnMouseDown()
	{
		TakeDamage(1f);
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
}
