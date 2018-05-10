using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEarthHealth : MonoBehaviour
{
    public Text healthBox; // textbox to display the earths health

    private void Start()
    {
        healthBox = GetComponent<Text>();
    }

    private void Update()
    {
        healthBox.text = "EARTH: " + EarthHealth.health;
        ChangeTextColour();
    }

    public void ChangeTextColour()
    {
        if (EarthHealth.health <= 250)
        {
            healthBox.color = Color.yellow;
            if (EarthHealth.health <= 50)
            {
                healthBox.color = Color.red;
            }
        }
    }
}
