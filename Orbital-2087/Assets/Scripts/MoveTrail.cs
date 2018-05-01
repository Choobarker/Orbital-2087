using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 100;
    public Transform Earth;
    //Transform FirePoint;
    Vector3 EarthDirection;


    void Awake()
    {
        if (Earth == null)
        {
            Debug.LogError("No Earth!");
        }
        
        EarthDirection = new Vector3(Earth.position.x, Earth.position.y, Earth.position.z); 
    }

    void Update () {

        transform.Translate(EarthDirection);//* Time.deltaTime * moveSpeed);
        Destroy(this.gameObject, 3);
	}
}
