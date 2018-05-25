using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEarthHealth : MonoBehaviour
{
    public Text healthBox; // textbox to display the earths health
    public EarthHealth earthHealth;

    private void Start()
    {
        healthBox = GetComponent<Text>();
        earthHealth = GameObject.FindGameObjectWithTag("Earth").GetComponent<EarthHealth>();
    }

    public void UpdateText(float health)
    {
        if (healthBox == null)
        {
            Start();
        }

        healthBox.text = health + "/" + earthHealth.GetStartingHealth();
    }
}
