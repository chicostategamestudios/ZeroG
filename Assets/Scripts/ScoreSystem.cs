using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour {

	public static ScoreSystem Instance;
	public int player1Score;
	public int player2Score;
	public int player3Score;
	public int player4Score;
	private Scene myScene;

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	void Update(){
		myScene = SceneManager.GetActiveScene();
		if (myScene.name == "GameStart") {
			player1Score = 0;
			player2Score = 0;
			player3Score = 0;
			player4Score = 0;
		}
	}
}
