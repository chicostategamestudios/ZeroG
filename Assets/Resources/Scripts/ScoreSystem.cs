using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour {

	public static ScoreSystem Instance;
	public int player1Score;
	public int player2Score;
	public int player3Score;
	public int player4Score;
	private Scene myScene;

	//Number of players
	public int num_players;

	//struct of a player's stats in a level
	class levelstats
	{
		//Time player finished the level (negative value is Did Not Finish)
		public float time;
		//Player score in that level. Should be a non-negative number. 
		public int score;
		//Number of player deaths. Should be a non-negative number. 
		public int deaths;
		//What place did they finish (1st place, 2nd place, etc.). Should be between 1 and max # of players. 
		public int place;
	}
	List< List<levelstats> > playerstats = new List< List<levelstats> >();
	
	//Increase the death count of argument int player in argument int level. 
	public void IncreaseDeath(int level, int player)
	{
		playerstats [player] [level].deaths += 1;
	}
	//Set the death count of argument int player in argument int level to argument int deaths. 
	public void SetDeath(int deaths, int level, int player)
	{
		playerstats [player] [level].deaths += deaths;
	}
	//Get the number of deaths of argument player in argument level
	public int GetDeath(int level, int player)
	{
		return playerstats [player] [level].deaths;
	}
	//Get the total number of deaths of argument player across all levels
	public int GetTotalDeaths(int player)
	{
		int total_deaths = 0;
		for (int i = 1; i < playerstats [player].Count() + 1; i++) 
		{
			total_deaths += playerstats [player] [i].deaths;
		}
		return total_deaths;
	}
	//Return the number of the player with the most total deaths. 
	public int MostTotalDeaths()
	{
		int max = GetTotalDeaths(1);
		int max_player = 1;
		for (int i = 2; i <= num_players; i++) 
		{
			if (GetTotalDeaths(i) > max) 
			{
				max_player = i;
				max = GetTotalDeaths (i);
			}
		}
		return max_player;
	}
	//Record the time of argument float time for argument int player in argument int level. 
	public void SetTime(float time, int level, int player)
	{
		playerstats [player] [level].time = time;
	}
	//Get the time of argument player in argument level
	public float GetTime(int level, int player)
	{
		return playerstats [player] [level].time;
	}
	//Return the total time of argument player across all levels
	public float GetTotalTime(int player)
	{
		float total_time = 0;
		for (int i = 1; i <= playerstats [player].Count; i++) 
		{
			total_time += playerstats [player] [i].time;
		}
		return total_time;
	}
	//Return player number of player with the slowest total time. Pass by reference that time. 
	public int SlowestTime(ref float time)
	{
		time = GetTotalTime(1);
		int slow_player = 1;
		for (int i = 2; i <= num_players; i++) 
		{
			if (GetTotalDeaths(i) > time) 
			{
				slow_player = i;
				time = GetTotalDeaths (i);
			}
		}
		return slow_player;
	}
	//Return player number of player with the fastest total time. Pass by reference that time. 
	public int FastestTime(ref float time)
	{
		time = GetTotalTime(1);
		int fast_player = 1;
		for (int i = 2; i <= num_players; i++) 
		{
			if (GetTotalDeaths(i) < time) 
			{
				fast_player = i;
				time = GetTotalDeaths (i);
			}
		}
		return fast_player;
	}
	//Record the score of argument int score for argument int player in argument int level.
	public void SetScore(int score, int level, int player)
	{
		playerstats [player] [level].score = score;
	}
	//Get the score of argument player in argument level
	public int GetScore(int level, int player)
	{
		return playerstats [player] [level].score;
	}
	public int GetTotalScore(int player)
	{
		int total_score = 0;
		for(int cur_level = 1; cur_level <= playerstats[player].Count; cur_level++)
		{
			total_score += playerstats[player][cur_level].score;
		}
		return total_score;
	}
	//Record the winning place of argument int place for argument int player in argument int level.
	public void SetPlace(int place, int level, int player)
	{
		playerstats [player] [level].place = place;
	}
	//Get the place of argument player in argument level
	public int GetPlace(int place, int level, int player)
	{
		return playerstats [player] [level].place;
	}
	//Return a player # if that player got 1st place every time. Otherwise, return -1. 
	public int King()
	{
		//For each player
		for (int cur_player = 1; cur_player <= num_players; cur_player++) 
		{
			bool is_king = true;
			//For each level
			for (int cur_level = 1; cur_level <= playerstats [cur_player].Count(); cur_level++) 
			{
				if (playerstats [cur_player] [cur_level].place != 1) 
				{
					is_king = false;
					break;
				}
			}
			if (is_king) 
			{
				return cur_player;
			}
		}
		return -1;
	}
	
	// Use this for initialization
	void Start () {
		//Initialize number of players
		num_players = 4;
		//Initialize playerstats for each player. 
		for (int i = 1; i < num_players; i++) 
		{
			playerstats [i] = new List<levelstats> ();	
		}
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
