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
    public AudioSource fireRateBoost;
    public AudioSource shieldBoost;
    public AudioSource speedBoost;

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

        bgMusic.mute = false;
        alienBullets.mute = false;
        playerBullets.mute = false;
        earthExplosion.mute = false;
        shipExplosion.mute = false;
        fireRateBoost.mute = false;
        shieldBoost.mute = false;
        speedBoost.mute = false;
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
        fireRateBoost.mute = !fireRateBoost.mute;
        shieldBoost.mute = !shieldBoost.mute;
        speedBoost.mute = !speedBoost.mute;
    }

    private void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
