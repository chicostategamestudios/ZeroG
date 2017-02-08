using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

	public Text MyTimerText;

	public float timer = 0.0f;
    public bool startTimer = false;

	// Use this for initialization
	void Start () {

		MyTimerText = this.GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (startTimer) {
            timer += Time.deltaTime;
            MyTimerText.text = TimeToString();
        }
	}

	private string TimeToString(){
		int minutes = Mathf.FloorToInt (timer / 60f);
		int seconds = Mathf.FloorToInt (timer % 60);
		float miliseconds = timer * 1000;
		miliseconds = miliseconds % 1000;

		string displayedTimer = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
		return displayedTimer;

	}
}
