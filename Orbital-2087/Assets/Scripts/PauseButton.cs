using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour 
{
    Button pauseButton;
    GameObject pauseMenu;

	void Start () 
    {
		pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(PauseClicked);
	}
	
	private void PauseClicked()
    {

    }
}
