using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : MonoBehaviour 
{
    private const float DURATION = 10;
    private const float MOVE_SPEED = 20;

    private PlayerHealth playerHealth;

	void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.timeScale / MOVE_SPEED);
    }
	
	public void Activate()
    {
        playerHealth.ActivateShield(DURATION);
    }
}