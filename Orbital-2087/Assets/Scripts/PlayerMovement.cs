using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    //public float tilt; //Unused atm
    public float circumference;
    private float moveVar;
    public Transform target;

    void Update()
    {
    }
    private void FixedUpdate()
    {
        //Controls player movement
        moveVar += CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * speed;//RA Remove inputgetaxis when building android ver.
        float x = Mathf.Sin(moveVar) * circumference;
        float y = Mathf.Cos(moveVar) * circumference;
        float z = 0;
        Vector3 move = new Vector3(x, y, z);
        transform.position = move;
        
        //Controls player rotation
        Vector2 direction = new Vector2(target.position.x - move.x, target.position.y - move.y);
        float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
        this.transform.eulerAngles = new Vector3(0, 0, rotation);
        
        //TODO Tilt?
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}



