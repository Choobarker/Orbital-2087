using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour 
{
    private const float DURATION = 10;
    private const float MULTIPLIER = 1.5f;
    private const float MOVE_SPEED = 20;

    private PlayerMovement playerMovement;

	void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.timeScale / MOVE_SPEED);
    }
    
    public void Activate()
    {
        playerMovement.ActivateSpeedBoost(DURATION, MULTIPLIER);
    }
}
