using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    int timer;
    public Text timerText;
    bool started = false;

	// Use this for initialization
	void Start () {
        timer = 11;
        timerText.text = timer.ToString();
        InvokeRepeating("startTimer", 0f, 1f);
    }
	
	// Update is called once per frame
    void Update() {

    }
    void startTimer() {
        
        timer -= 1;
        if(timer > 0)
            timerText.text = timer.ToString();
        if(timer <= 0) {
            GameOptions.Instance.LoadLevel();
            Debug.Log("HEYA!");
            timerText.text = "LOADING...";
        }
    }
}
