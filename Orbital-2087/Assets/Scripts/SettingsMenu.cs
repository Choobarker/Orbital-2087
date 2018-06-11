using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour 
{
    public AudioSource bgMusic;
    public AudioSource alienBullets;
    public AudioSource playerBullets;
    public AudioSource earthExplosion;
    public AudioSource shipExplosion;

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public Button soundEffectsToggle;
    public Button musicToggle;
    public Button back;

    void Start()
    {
        soundEffectsToggle.onClick.AddListener(SoundEffectsToggle);
        musicToggle.onClick.AddListener(MusicToggle);
        back.onClick.AddListener(Back);
    }

    private void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void MusicToggle()
    {
        bgMusic.mute = !bgMusic.mute;
    }

    private void SoundEffectsToggle()
    {
        alienBullets.mute = !alienBullets.mute;
        playerBullets.mute = !playerBullets.mute;
        earthExplosion.mute = !earthExplosion.mute;
        shipExplosion.mute = !shipExplosion.mute;
    }

    private void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
