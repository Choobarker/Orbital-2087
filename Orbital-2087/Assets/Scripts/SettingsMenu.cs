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
    public Button accelorometerToggle;
    public Button back;

    bool acceloOn = false;
    bool musicOn = true;
    bool effectsOn = true;

    PlayerMovement playerMovement;

    void Start()
    {
        soundEffectsToggle.onClick.AddListener(SoundEffectsToggle);
        musicToggle.onClick.AddListener(MusicToggle);
        accelorometerToggle.onClick.AddListener(AccelorometerToggle);
        back.onClick.AddListener(Back);

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        acceloOn = playerMovement.getAcceloToggle();

        musicToggle.GetComponent<Image>().color = Color.green;
        soundEffectsToggle.GetComponent<Image>().color = Color.green;
        accelorometerToggle.GetComponent<Image>().color = Color.green;

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
        
        musicOn = !musicOn;

        if(!musicOn)
        {
            musicToggle.GetComponent<Image>().color = Color.red;
        }
        else
        {
            musicToggle.GetComponent<Image>().color = Color.green;
        }
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

        effectsOn = !effectsOn;

        if(!effectsOn)
        {
            soundEffectsToggle.GetComponent<Image>().color = Color.red;
        }
        else
        {
            soundEffectsToggle.GetComponent<Image>().color = Color.green;
        }
    }

    private void AccelorometerToggle()
    {
        playerMovement.TogglePlayerAccelorometer();
        acceloOn = !acceloOn;

        if(!acceloOn)
        {
            accelorometerToggle.GetComponent<Image>().color = Color.red;
        }
        else
        {
            accelorometerToggle.GetComponent<Image>().color = Color.green;
        }
    }

    private void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}