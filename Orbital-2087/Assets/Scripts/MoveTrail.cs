using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour 
{
    public int moveSpeed;

    public Transform firePoint;
    public Transform earth;
    public LayerMask whatToHit;    

    void Update () 
    {
        transform.Translate(Vector3.down * Time.timeScale / moveSpeed);
        Destroy(this.gameObject, 10);
    }
}
