using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNotifier : MonoBehaviour 
{
    public static int waveNum = 1;
    Text wave;
  
    void Start()
    {
        waveNum = 1;
        wave = GetComponent<Text>();
    }
  
    void Update()
    {
        wave.text = "W A V E :  " + waveNum;
    }
    
    public static void IncrementWave()
    {
        ++waveNum;
    }
}