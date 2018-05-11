using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

	
	void Start ()
    {
		
	}
	

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pauseGame();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            continueGame();
        }
	}

    private void pauseGame()
    {
        Time.timeScale = 0;

    }

    private void continueGame()
    {
        Time.timeScale = 1;
    }

}
