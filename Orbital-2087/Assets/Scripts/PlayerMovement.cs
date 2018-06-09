using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public bool playerJoystickControl;
    private bool speedBoostActive = false;

    public float speed;
    public float radius = 3;
    private float moveVar;
    private float boostDurationLeft = 0;
    private float boostMultiplier = 0;
    private bool isMoveLeft, isMoveRight = false;

    public Transform earth;
    public Transform midSpace;

    public GameObject joystickButton, movementArrows;

    private Vector3 lastPos;

    private BoostTimerController btc;

    void Start()
    {
        btc = gameObject.GetComponent<BoostTimerController>();
        lastPos = transform.position;
    }

    public Vector3 GetNextLocation(float moveVar)
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
            joystickButton.SetActive(true);
            movementArrows.SetActive(false);

            moveVar += CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * speed; //Joystick controller
            moveVar += Input.acceleration.x * Time.deltaTime * speed; //Accelerometer controller. TEMPORARY
        }
        else if (!playerJoystickControl)
        {
            movementArrows.SetActive(true);
            joystickButton.SetActive(false);

            //moveVar += Input.acceleration.x * Time.deltaTime * speed; //Accelerometer controller
            moveVar += Input.GetAxis("Horizontal") * Time.deltaTime * speed/3.0f; //RA Remove inputgetaxis when building final android ver.

            if(isMoveLeft)
            {
                moveVar -= 0.35f * Time.deltaTime * speed;
            }
            if(isMoveRight)
            {
                moveVar += 0.35f * Time.deltaTime * speed;
            }
        }

        transform.position = GetNextLocation(moveVar);
        midSpace.Translate(GetMoveDifference(GetNextLocation(moveVar)));

        if(earth != null)
        {
            //Controls player rotation
            Vector2 direction = new Vector2(earth.position.x - GetNextLocation(moveVar).x, earth.position.y - GetNextLocation(moveVar).y); // Uses Earth as the point where the ship faces outward from
            float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90; //+90 or the sprite doesn't face outwards
            this.transform.eulerAngles = new Vector3(0, 0, rotation);
        }
    }

    public void OnPressLeft()
    {
        isMoveLeft = true;
    }
    public void OnReleaseLeft()
    {
        isMoveLeft = false;
    }
    public void OnPressRight()
    {
        isMoveRight = true;
    }
    public void OnReleaseRight()
    {
        isMoveRight = false;
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

    Vector3 GetMoveDifference(Vector3 newPos)
    {
        float x = (lastPos.x - newPos.x) / 2;
        float y = (lastPos.y - newPos.y) / 2;

        lastPos = newPos;

        return new Vector3(x, y, 0);
    }
}
