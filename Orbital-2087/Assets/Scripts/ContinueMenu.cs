using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueMenu: MonoBehaviour
{
    private Button pauseButton;

    void Start()
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(PlayClicked);
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
