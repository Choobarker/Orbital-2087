using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Text healthBox; // textbox to display the players health
    PlayerHealth maxHealth;

    void Start()
    {
        healthBox = GetComponent<Text>();
        maxHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void UpdateText(float health)
    {
        if(healthBox == null)
        {
            Start();
        }

        healthBox.text = health + "/" + maxHealth.GetMaxHealth();
    }

}
