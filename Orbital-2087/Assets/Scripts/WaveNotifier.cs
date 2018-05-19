using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNotifier : MonoBehaviour {

    public static int waveNum = 1;
    Text wave;

    // Use this for initialization
    void Start()
    {
        waveNum = 1;
        wave = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        wave.text = "W A V E :  " + waveNum;
    }

}
