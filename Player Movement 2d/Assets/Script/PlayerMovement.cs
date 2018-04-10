﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;

    float moveVar;
    public float circumference;

    void Update()
    {

    }

    void FixedUpdate()
    {
        moveVar -= Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float x = Mathf.Cos(moveVar) * circumference;
        float y = Mathf.Sin(moveVar) * circumference;
        float z = 0;
        transform.position = new Vector3(x, y, z);
        Vector3 ridY = transform.position;
        ridY.x = 0.0f;
        transform.rotation = Quaternion.LookRotation(ridY);

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}


