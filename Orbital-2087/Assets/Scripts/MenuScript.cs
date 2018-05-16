using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
    }

    public bool gamePaused = false;

    public void PauseGame()
    {    
        if (!gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }
}
