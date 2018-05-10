using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Text healthBox; // textbox to display the players health

    private void Start()
    {
        healthBox = GetComponent<Text>();
    }

    private void Update()
    {
        healthBox.text = "HEALTH: " + PlayerHealth.health;
    }
}
