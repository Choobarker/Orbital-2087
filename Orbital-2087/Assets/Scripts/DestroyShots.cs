using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShots : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}
