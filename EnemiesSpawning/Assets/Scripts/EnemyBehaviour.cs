using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 2f;
	public float speed = 2f;
	private bool inPosition = false;
	public float moveTime = 2f;
	private float timeMoved = 0f;
	
	void start()
	{
		Debug.Log("Enemy Behaviour started");
	}
	
	void Update () 
	{		
		while(!inPosition)
		{
			transform.position += Vector3.forward * Time.deltaTime * speed;
			timeMoved += Time.deltaTime;

			if(timeMoved >= moveTime)
			{
				inPosition = true;
			}
		}
	}
}
