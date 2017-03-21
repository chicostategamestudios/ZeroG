using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour {

	public static ScoreSystem Instance;
	private Scene myScene;
	public int current_level;
	public Dictionary<int, Player> player = new Dictionary<int, Player>();

	public class Player
	{
		class Level
		{
			public float time;
			public int place;
		}
		Dictionary<int,Level> levels = new Dictionary<int, Level> ();
		int deaths;
		int score;

		//Public Functions
		//Death Get/Set and add 1 functions
		public Player()
		{
			deaths = 0;
			score = 0;
		}
		public void Dies(){deaths += 1;}
		public void SetDeaths (int amount){deaths = amount;}
		public int GetDeaths (){return deaths;}
		//Time Get/Set functions
		public void SetTime (int level, float time){levels [level].time = time;}
		public float GetTime (int level){return levels [level].time;}
		//Place Get/Set functions
		public void SetPlace (int level, int place){levels [level].place = place;}
		public int GetPlace (int level){
			return levels [level].place;
		}
		//Score Get/Set and Add functions
		public void SetScore (int amount) {score = amount;}
		public void AddScore (int amount) {score += amount;}
		public int GetScore(){return score;}
	}
		
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
		current_level = -1;
	}

	void Update(){
		myScene = SceneManager.GetActiveScene();
		if (myScene.name == "GameStart") {
			//Make players
			if (current_level != 0) 
			{
				current_level = 0;
				ScoreSystem.Instance.player.Add (1, new Player ());
				ScoreSystem.Instance.player.Add (2, new Player ());
				ScoreSystem.Instance.player.Add (3, new Player ());
				ScoreSystem.Instance.player.Add (4, new Player ());
			}
		}
	}
}
