using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Text healthBox; // textbox to display the players health

    void Start()
    {
        healthBox = GetComponent<Text>();
    }

/*
    private void Update()
    {
        healthBox.text = "HEALTH: " + playerHealth.getHealth();
        ChangeTextColour();
        Debug.Log("ahhh");
    }
*/

    public void UpdateText(float health)
    {
        if(healthBox == null)
        {
            Start();
        }

        healthBox.text = health + "/100";
        //UpdateTextColour(health);
    }

    //changes the colour of the text to help alert the player of their current health
    //void UpdateTextColour(float health)
    //{
    //    if (health <= 50)
    //    {
    //        healthBox.color = Color.yellow;

    //        if (health <= 25)
    //        {
    //            healthBox.color = Color.red;
    //        }           
    //    }
    //}
}
