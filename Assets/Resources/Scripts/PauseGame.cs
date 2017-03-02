using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseGame : MonoBehaviour {
	private bool paused;
	private GameObject pauseScreen;
	// Use this for initialization
	void Start () {
		paused = false;
		pauseScreen = GameObject.Find ("startMenu");
		pauseScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {			
			if (Input.GetButtonDown ("Start")) {
				Pause ();
			}
	}

	public void Pause(){
		if (!paused) {
			pauseScreen.SetActive (true);
			Time.timeScale = 0;
			paused = true;
		} else {
			Time.timeScale = 1;
			paused = false;
		}
	}
}
