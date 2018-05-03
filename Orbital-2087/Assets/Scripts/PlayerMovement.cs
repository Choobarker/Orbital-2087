using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public bool playerJoystickControl;
    public float speed;
    public float radius;
    private float moveVar;
    public Transform earth;
    
    private void FixedUpdate()
    {
        //Controls player movement
        if (playerJoystickControl)
        {
            moveVar += CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * speed; //Joystick controller
        }
        else if (!playerJoystickControl)
        {
            moveVar += Input.acceleration.x * Time.deltaTime * speed; //Accelerometer controller
            moveVar += Input.GetAxis("Horizontal") * Time.deltaTime * speed/3.0f; //Remove inputgetaxis when building android ver.

        }
        float x = Mathf.Sin(moveVar) * radius; //Used to make player rotate on the circular axis
        float y = Mathf.Cos(moveVar) * radius;
        float z = 0; //Keeps player ship on a 2d axis.
        Vector3 move = new Vector3(x, y, z);
        transform.position = move;

        //Debug.Log(move);
        
        //Controls player rotation
        Vector2 direction = new Vector2(earth.position.x - move.x, earth.position.y - move.y);
        float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90; //+90 or the sprite doesn't face outwards
        this.transform.eulerAngles = new Vector3(0, 0, rotation);
        
        //TODO Tilt?
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
