using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Sprite pauseSprite;
    public Sprite playSprite;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool gamePaused = false;
    private Image pauseButtonImage;

    // Use this for initialization
    void Start () {
        
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if (pauseButtonImage == null)
        {
            Start();
        }

        if (!gamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            gamePaused = true;
            pauseButtonImage.sprite = playSprite;
        }

    }
    public void ResumeGame()
    {
        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            gamePaused = false;
            pauseButtonImage.sprite = pauseSprite;
        }
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }
}
