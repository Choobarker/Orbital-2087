﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
    public Sprite pauseSprite;
    public Sprite playSprite;
    public AudioSource Audio;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool gamePaused = false;

    private Image pauseButtonImage;

    void Start()
    {
        pauseButtonImage = gameObject.GetComponent<Image>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
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
        //else if (gamePaused)
        //{
        //    pauseMenu.SetActive(false);
        //    Time.timeScale = 1;
        //    gamePaused = false;
        //    pauseButtonImage.sprite = pauseSprite;
        //}
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

    public void MusicToggle()
    {
        Audio.mute = !Audio.mute;
    }

    public void AudioToggle()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void AccelerometerToggle()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
