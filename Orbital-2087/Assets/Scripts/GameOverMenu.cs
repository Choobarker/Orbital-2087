using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
    private Button continueButton;

    AsyncOperation async;

	IEnumerator Start () 
    {
		continueButton = gameObject.GetComponent<Button>();
        continueButton.onClick.AddListener(Continue);

        async = SceneManager.LoadSceneAsync("MainLevel");
        async.allowSceneActivation = false;
        yield return async;
    }

    private void Continue()
    {
        async.allowSceneActivation = true;
    }
}
