using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool gamePaused = false;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (!gamePaused)
        {
            pauseGame();
            gamePaused = true;
        }
        else if (gamePaused)
        {
            continueGame();
            gamePaused = false;
        }
    }

    void Update()
    {

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
