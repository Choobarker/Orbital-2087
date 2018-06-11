using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour 
{
    Text Score;
    
    void Start () 
    {
        Score = GetComponent<Text>();
        Score.text = "S C O R E :  " + ScoreKeeping.GetScore();
        ScoreKeeping.ResetScore();
    }
}
