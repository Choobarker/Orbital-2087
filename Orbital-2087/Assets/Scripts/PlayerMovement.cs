using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;

    float moveVar;//Edits
    public float circumference;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    //Instantiate the players shot
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    void FixedUpdate()
    {
        moveVar += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Mathf.Cos(moveVar) * circumference;
        float z = 0;
        float x = Mathf.Sin(moveVar) * circumference;
        transform.position = new Vector3(x, y, z);
        //transform.rotation = Quaternion.LookRotation(transform.position);

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}



