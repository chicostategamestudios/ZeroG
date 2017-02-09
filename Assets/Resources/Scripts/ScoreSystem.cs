using UnityEngine;
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
