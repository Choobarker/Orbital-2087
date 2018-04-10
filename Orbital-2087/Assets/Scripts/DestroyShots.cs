using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShots : MonoBehaviour 
{
    //When gameObjects collide with boundary, destroy them.
	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}
