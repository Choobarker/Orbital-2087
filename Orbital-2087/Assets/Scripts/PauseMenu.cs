using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    private bool gamePaused = false;

    public GameObject settingsMenu;
    public GameObject pauseMenu;

    public Button settings;
    public Button resume;
    public Button pause;

    public Sprite pauseSprite;
    public Sprite playSprite;

    private Image pauseButtonImage;

	// Use this for initialization
	void Start () 
    {
        pauseButtonImage = pause.gameObject.GetComponent<Image>();
        
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        settings.onClick.AddListener(OpenSettingsMenu);
        resume.onClick.AddListener(ResumeGame);
	}

    public void OpenSettingsMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if(pauseButtonImage == null)
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

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
        pauseButtonImage.sprite = pauseSprite;
    }
}
