//This script is used to keep track of player stats such as score, deaths, and times. 

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
			public Level()
			{
				time = -1;
				place = -1;
			}
		}
		Dictionary<int,Level> levels = new Dictionary<int, Level> ();
		int deaths;
		int score;
		string trophy;

		//Public Functions
		//Death Get/Set and add 1 functions
		public Player()
		{
			deaths = 0;
			score = 0;
			trophy = "No Trophy";
		}
		public void Dies(){deaths += 1;}
		public void SetDeaths (int amount){deaths = amount;}
		public int GetDeaths (){return deaths;}
		//Time Get/Set functions
		public void SetTime (int level, float time){levels [level].time = time;}
		public float GetTime (int level){return levels [level].time;}
		public float GetTotalTime()
		{
			float total_time = 0;
			//For every Level in the dictionary of levels
			foreach (KeyValuePair<int, Level> cur_level in levels) 
			{
				total_time += cur_level.Value.time;
			}
			return total_time;
		}
		//Place Get/Set functions
		public void SetPlace (int level, int place){levels [level].place = place;}
		public int GetPlace (int level){
			return levels [level].place;
		}
		//Score Get/Set and Add functions
		public void SetScore (int amount) {score = amount;}
		public void AddScore (int amount) {score += amount;}
		public int GetScore(){return score;}
		//Trophy Get/Set functions
		public void SetTrophy (string trophy_text) {trophy = trophy_text;}
		public string GetTrophy () {return trophy;}
		//Add Level
		public void AddLevel(int level)
		{
			levels.Add (level, new Level ());
		}
		//Misc functions
		//Check if player won every match
		public int IsKing()
		{
			int isKing = 1;
			//For every Level in the dictionary of levels
			foreach (KeyValuePair<int, Level> cur_level in levels) 
			{
				if (cur_level.Value.place != 1) 
				{
					isKing = 0;
				}
			}
			return isKing;
		}
	}

	public void SetTrophies()
	{
		int fastest = 0;
		int slowest = 0;
		int died_most = 0;
		float fastest_time = 99999999;
		float slowest_time = 0;
		int most_deaths = 0;
		int king = 0;
		for (int cur_player = 1; cur_player <= 4; cur_player++) {
			float mytime = player [cur_player].GetTotalTime ();
			if (mytime < fastest_time) 
			{
				fastest = cur_player;
				fastest_time = mytime;
			}
			if (mytime > slowest_time) 
			{
				slowest = cur_player;
				slowest_time = mytime;
			}
			if (player [cur_player].GetDeaths () > most_deaths) 
			{
				died_most = cur_player;
				most_deaths = player [cur_player].GetDeaths ();
			}

			if (player [cur_player].IsKing () == 1) 
			{
				king = cur_player;
			}
		}
		if(slowest != 0)
			player [slowest].SetTrophy ("Slowest");
		if(fastest != 0)
			player [fastest].SetTrophy ("Fastest");
		if(died_most != 0)
			player [died_most].SetTrophy ("Clumsy");
		if(king != 0)
			player [king].SetTrophy ("King");
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
