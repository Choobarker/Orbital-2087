using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
    public AudioSource Audio;
    public AudioSource AlienBullets;
    public AudioSource playerBullets;
    public AudioSource Earth;
    public AudioSource Player;
    
    void Start()
    {
        AlienBullets.mute = !AlienBullets.mute;
        playerBullets.mute = !playerBullets.mute;
        Earth.mute = !Earth.mute;
        Player.mute = !Player.mute;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
    }

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
