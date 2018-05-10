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
        ChangeTextColour();
    }

    //changes the colour of the text to help alert the player of their current health
    public void ChangeTextColour()
    {
        if (PlayerHealth.health <= 50)
        {
            healthBox.color = Color.yellow;
            if (PlayerHealth.health <= 25)
            {
                healthBox.color = Color.red;
            }           
        }
    }
}
