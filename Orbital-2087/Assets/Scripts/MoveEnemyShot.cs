using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyShot : MonoBehaviour 
{
    public int moveSpeed;  

    void Update () 
    {
        transform.Translate(Vector3.down * Time.timeScale / moveSpeed);
        Destroy(this.gameObject, 10);
    }
}
