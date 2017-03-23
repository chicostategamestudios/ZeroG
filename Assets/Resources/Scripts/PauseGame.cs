using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseGame : MonoBehaviour {
	[HideInInspector]public bool paused;
	private GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		paused = false;
		pauseScreen = GameObject.Find ("startMenu");
		if (SceneManager.GetActiveScene().name != "GameStart") {
			pauseScreen.SetActive (false);
		}
	}
	
	// Check if the players hit start during play
	void Update () {			
		if (Input.GetButtonDown ("Start") && SceneManager.GetActiveScene().name != "GameStart") {
				Pause ();
			}
	}

	//When game is paused turns on startScreen in canvas and sets timescale to 0
	public void Pause(){
		if (!paused) {
			//FMOD
			//Sound for entering game pause
			pauseScreen.SetActive (true);
			Time.timeScale = 0;
			paused = true;
		} else {
			//Sound for exiting game pause
			Time.timeScale = 1;
			paused = false;
		}
	}
}
