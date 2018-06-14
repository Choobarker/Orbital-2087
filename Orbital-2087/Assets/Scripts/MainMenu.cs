﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    private Button pauseButton;

    AsyncOperation async;

    IEnumerator Start() 
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(PlayClicked);

        async = SceneManager.LoadSceneAsync("MainLevel");
        async.allowSceneActivation = false;
        yield return async;
    }

    private void PlayClicked()
    {
        async.allowSceneActivation = true;
    }
}
