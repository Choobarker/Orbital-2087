using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployShield : MonoBehaviour {

	public GameObject earthShield;
	public Transform player;
    Vector3 shieldPos;

    void Start()
    {
        shieldPos = new Vector3(player.position.x, player.position.y - 10, 0);
    }

    public void Deploy()
    {
        if (shieldPos == null)
        {
            Start();
        }
        Instantiate(earthShield, player);
    }
}
