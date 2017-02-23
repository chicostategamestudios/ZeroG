using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Collections.Generic;

public class ScoreCheck : MonoBehaviour {

	public List<int> scores = new List<int>();
	public Transform p1FirstPlace;
	public Transform p1SecondPlace;
	public Transform p1ThirdPlace;
	public Transform p1FourthPlace;
	public Transform p2FirstPlace;
	public Transform p2SecondPlace;
	public Transform p2ThirdPlace;
	public Transform p2FourthPlace;
	public Transform p3FirstPlace;
	public Transform p3SecondPlace;
	public Transform p3ThirdPlace;
	public Transform p3FourthPlace;
	public Transform p4FirstPlace;
	public Transform p4SecondPlace;
	public Transform p4ThirdPlace;
	public Transform p4FourthPlace;
	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;
	public GameObject p1Score;
	public GameObject p2Score;
	public GameObject p3Score;
	public GameObject p4Score;
	public Text firstPlace;
	public Text secondPlace;
	public Text thirdPlace;
	public Text fourthPlace;
	public GameObject firstPlaceSprite;
	public GameObject secondPlaceSprite;
	public GameObject thirdPlaceSprite;
	public GameObject fourthPlaceSprite;
	public Sprite adventurer;
	public Sprite wizard;
	public Sprite knight;
	public Sprite pirate;
	Dictionary<int, string> trophies = new Dictionary<int, string>();
	Text P1TrophyText;
	Text P2TrophyText;
	Text P3TrophyText;
	Text P4TrophyText;
	ScoreSystem stats;

	// Use this for initialization
	void Start () {
		/*for (int i = 1; i <= 4; i++) 
		{
			trophies.Add (i, "No Trophy");
		}*/
	}

	void CheckScore(){
		AssignTrophy ();
		int first;
		int second;
		int third;
		int fourth;
		int player1Score = ScoreSystem.Instance.player1Score;
		int player2Score = ScoreSystem.Instance.player2Score;
		int player3Score = ScoreSystem.Instance.player3Score;
		int player4Score = ScoreSystem.Instance.player4Score;
		scores.Add (player1Score);
		scores.Add (player2Score);
		scores.Add (player3Score);
		scores.Add (player4Score);
		first = scores.Max ();
		fourth = scores.Min ();
		scores.Remove (scores.Max ());
		scores.Remove (scores.Min ());
		second = scores.Max ();
		third = scores.Min ();

		//if (player_score == first) {
		//	player_object.transform.position = 
		//}

		if(SceneManager.GetActiveScene().name == "PlayerStanding"){
			//player1
			if (player1Score == first) {
				p1.transform.position = p1FirstPlace.transform.position;
			} else if (player1Score == second) {
				p1.transform.position = p1SecondPlace.transform.position;
			} else if (player1Score == third) {
				p1.transform.position = p1ThirdPlace.transform.position;
			} else {
				p1.transform.position = p1FourthPlace.transform.position;
			}
			//player2
			if (player2Score == first) {
				p2.transform.position = p2FirstPlace.transform.position;
			} else if (player2Score == second) {
				p2.transform.position = p2SecondPlace.transform.position;
			} else if (player2Score == third) {
				p2.transform.position = p2ThirdPlace.transform.position;
			} else {
				p2.transform.position = p2FourthPlace.transform.position;
			}
			//player3
			if (player3Score == first) {
				p3.transform.position = p3FirstPlace.transform.position;
			} else if (player3Score == second) {
				p3.transform.position = p3SecondPlace.transform.position;
			} else if (player3Score == third) {
				p3.transform.position = p3ThirdPlace.transform.position;
			} else {
				p3.transform.position = p3FourthPlace.transform.position;
			}
			//player4
			if (player4Score == first) {
				p4.transform.position = p4FirstPlace.transform.position;
			} else if (player4Score == second) {
				p4.transform.position = p4SecondPlace.transform.position;
			} else if (player4Score == third) {
				p4.transform.position = p4ThirdPlace.transform.position;
			} else {
				p4.transform.position = p4FourthPlace.transform.position;
			}
		}else{

			//player1
			DisplayScoreSprite(1, player1Score, adventurer, first, second, third);
			DisplayScoreSprite(2, player2Score, wizard, first, second, third);
			DisplayScoreSprite(3, player3Score, knight, first, second, third);
			DisplayScoreSprite(4, player4Score, pirate, first, second, third);
		}
	}

	void DisplayScoreSprite(int player_number, int player_score, Sprite player_sprite, int first, int second, int third)
	{
		if (player_score == first) 
		{
			firstPlaceSprite.GetComponent<Image>().sprite = player_sprite;
			firstPlace.text = player_score + " Points " + trophies [player_number];
		}
		else if (player_score == second) 
		{
			secondPlaceSprite.GetComponent<Image>().sprite = player_sprite;
			secondPlace.text = player_score + " Points " + trophies [player_number];
		}
		else if (player_score == third) 
		{
			thirdPlaceSprite.GetComponent<Image>().sprite = player_sprite;
			thirdPlace.text = player_score + " Points " + trophies [player_number];
		}
		else
		{
			fourthPlaceSprite.GetComponent<Image>().sprite = player_sprite;
			fourthPlace.text = player_score + " Points " + trophies [player_number];
		}
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "PlayerStanding") 
		{
			p1Score.GetComponent<TextMesh> ().text = "" + ScoreSystem.Instance.player1Score + " Points";
			p2Score.GetComponent<TextMesh> ().text = "" + ScoreSystem.Instance.player2Score + " Points";
			p3Score.GetComponent<TextMesh> ().text = "" + ScoreSystem.Instance.player3Score + " Points";
			p4Score.GetComponent<TextMesh> ().text = "" + ScoreSystem.Instance.player4Score + " Points";
		}
	}
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (SceneManager.GetActiveScene ().name == "PlayerStanding" || SceneManager.GetActiveScene().name == "Victory") {
			CheckScore ();
		}
	}

	//Assign each player a trophy
	void AssignTrophy()
	{
		for (int i = 1; i <= 4; i++) {
			trophies [i] = "No Trophy";
		}

		//Clumsy - Most Deaths
		//trophies [stats.MostTotalDeaths()] = "Clumsy - Most Deaths";
		//Slowpoke - Slowest Time: Insert Time Here
		//float slowtime = 0;
		//trophies.Add(stats.SlowestTime(ref slowtime), "Slowpoke - Slowest Time: ");
		//Flash - Fastest Time
		//float fasttime = 0;
		//trophies.Add (stats.FastestTime (ref fasttime), "Flash - Fastest Time: ");
		//King - Finished First on All Maps
		//if (stats.King () != -1) 
		//{
		//	trophies.Add (stats.King (), "King - Finished First on ALL Maps");
		//}
		////Note to self: Not sure if slowtime updates from pass by reference before string is appened. Need to test. -Brandon
		////Note to self: stat functions appear to be broken. Causing an error when trying to get info. May be because there is no info to get; try testing from start of game. -B
		////Note to self: Ask Phil if there is a time conversion function already made. -B

	}
		
}
