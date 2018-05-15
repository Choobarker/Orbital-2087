﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public bool playerJoystickControl;
    public bool speedBoostActive = false;

    public float speed;
    public float radius = 3;
    private float moveVar;
    private float boostDurationLeft = 0;
    private float boostMultiplier = 0;

    public Transform earth;

    private BoostTimerController btc;

    void Start()
    {
        btc = gameObject.GetComponent<BoostTimerController>();
    }

    public Vector3 getNextLocation(float moveVar)
    {
        float x = Mathf.Sin(moveVar) * radius; //Used to make player rotate on the circular axis
        float y = Mathf.Cos(moveVar) * radius;
        float z = 0f;
        Vector3 move = new Vector3(x, y, z);
        
        return move;
    }

    private void FixedUpdate()
    {
        if(speedBoostActive)
        {
            boostDurationLeft -= Time.deltaTime;

            if(boostDurationLeft <= 0)
            {
                speedBoostActive = false;
                speed /= boostMultiplier;
            }

            btc.UpdateSpeedTimer(boostDurationLeft);
        }
        
        //Controls player movement
        if (playerJoystickControl)
        {
            moveVar += CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * speed; //Joystick controller
            moveVar += Input.acceleration.x * Time.deltaTime * speed; //Accelerometer controller. TEMPORARY

        }
        else if (!playerJoystickControl)
        {
            //moveVar += Input.acceleration.x * Time.deltaTime * speed; //Accelerometer controller
            moveVar += Input.GetAxis("Horizontal") * Time.deltaTime * speed/3.0f; //RA Remove inputgetaxis when building final android ver.
        }

        transform.position = getNextLocation(moveVar);

        if(earth != null)
        {
            //Controls player rotation
            Vector2 direction = new Vector2(earth.position.x - getNextLocation(moveVar).x, earth.position.y - getNextLocation(moveVar).y); // Uses Earth as the point where the ship faces outward from
            float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90; //+90 or the sprite doesn't face outwards
            this.transform.eulerAngles = new Vector3(0, 0, rotation);
        }
    }

    public void ActivateSpeedBoost(float duration, float boostMultiplier)
    {
        if(!speedBoostActive)
        {
            speedBoostActive = true;
            speed *= boostMultiplier;
            this.boostMultiplier = boostMultiplier;
            boostDurationLeft = duration + Time.deltaTime;
        }
        else
        {
            boostDurationLeft += duration;
        }

        btc.SetSpeedDuration(boostDurationLeft);     
    }
}
