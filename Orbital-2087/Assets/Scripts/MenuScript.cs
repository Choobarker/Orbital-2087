using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
    public Sprite pauseSprite;
    public Sprite playSprite;
    public AudioSource Audio;

    private Image pauseButtonImage;

    void Start()
    {
        pauseButtonImage = gameObject.GetComponent<Image>();
    }

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
        if(pauseButtonImage == null)
        {
            Start();
        }

        if (!gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
            pauseButtonImage.sprite = playSprite;
        }
        else if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
            pauseButtonImage.sprite = pauseSprite;
        }
    }

    public AudioSource AlienBullets;
    public AudioSource playerBullets;
    public AudioSource Earth;
    public AudioSource Player;


    public void MusicToggle()
    {
        Audio.mute = !Audio.mute;
    }

    public void AudioToggle()
    {
        AlienBullets.mute = !AlienBullets.mute;
        playerBullets.mute = !playerBullets.mute;
        Earth.mute = !Earth.mute;
        Player.mute = !Player.mute;
    }

    public void AccelerometerToggle()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
