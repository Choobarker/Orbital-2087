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

/*
    private void Update()
    {
        healthBox.text = "EARTH: " + EarthHealth.health;
        UpdateTextColour(3);
    }
*/

    public void UpdateText(float health)
    {
        if(healthBox == null)
        {
            Start();
        }

        healthBox.text = health + "/500";
        //UpdateTextColour(health);
    }

    //public void UpdateTextColour(float health)
    //{
    //    if (health <= 250)
    //    {
    //        healthBox.color = Color.yellow;
    //        if (health <= 50)
    //        {
    //            healthBox.color = Color.red;
    //        }
    //    }
    //}
}
