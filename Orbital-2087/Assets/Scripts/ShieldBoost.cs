using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : MonoBehaviour 
{
    private const float DURATION = 10;

    private PlayerHealth playerHealth;

	void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
	
	public void Activate()
    {
        playerHealth.ActivateShield(DURATION);
    }
}
