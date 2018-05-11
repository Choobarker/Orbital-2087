using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour 
{
    private const float DURATION = 10;
    private const float MULTIPLIER = 1.5f;

    private PlayerMovement playerMovement;

	void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    public void Activate()
    {
        playerMovement.ActivateSpeedBoost(DURATION, MULTIPLIER);
    }
}
