using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed;
    public Transform firePoint;
    public Transform earth;
    public LayerMask whatToHit;
    public float damage = 10;



    void Update () {
        
        transform.Translate(Vector3.down / moveSpeed);
        Destroy(this.gameObject, 2);
        Vector2 earthPosition = new Vector2(earth.position.x, earth.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, earthPosition - firePointPosition, 100, whatToHit);

        if (hit.collider != null)
        {
            Destroy(this.gameObject);
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");

        }
    }
}
