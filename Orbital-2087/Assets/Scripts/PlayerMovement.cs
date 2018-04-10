using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;

    float moveVar;//Edits
    public float circumference;

    void Update()
    {

    }

    void FixedUpdate()
    {
        moveVar -= Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float x = Mathf.Cos(moveVar) * circumference;
        float y = 0;
        float z = Mathf.Sin(moveVar) * circumference;
        transform.position = new Vector3(x, y, z);
        transform.rotation = Quaternion.LookRotation(transform.position);

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}



