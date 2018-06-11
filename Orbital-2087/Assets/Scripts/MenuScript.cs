using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
    public Sprite pauseSprite;
    public Sprite playSprite;

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
}
