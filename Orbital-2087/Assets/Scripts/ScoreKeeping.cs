using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour
{
	private const float CASH_MULT = 0.5F;
    
    private static float score = 0;
    private static float cash = 0;    

    public Text scoreText;

    void Start()
    {
        UpdateScore();
    }
    
	void UpdateScore()
    {
        scoreText.text = "S C O R E :  " + score;
	}

    public void AddScore(float addScore)
    {
        score += addScore;
        cash += addScore * CASH_MULT;

        UpdateScore();
    }

    public static float GetScore()
    {
        return score;
    }

    public float GetCash()
    {
        return cash;
    }

    public void SetCash(float newCash)
    {
        cash = newCash;
    }

    public static void ResetScore()
    {
        score = 0;
        cash = 0;
    }
}