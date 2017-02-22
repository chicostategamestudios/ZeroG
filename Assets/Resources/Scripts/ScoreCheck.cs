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
			if (player1Score == first) {
				firstPlaceSprite.GetComponent<Image>().sprite = adventurer;
				firstPlace.text = player1Score+" Points " + trophies[1];
			} else if (player1Score == second) {
				secondPlaceSprite.GetComponent<Image>().sprite = adventurer;
				secondPlace.text = player1Score+" Points " + trophies[1];
			} else if (player1Score == third) {
				thirdPlaceSprite.GetComponent<Image>().sprite = adventurer;
				thirdPlace.text = player1Score+" Points " + trophies[1];
			} else {
				fourthPlaceSprite.GetComponent<Image>().sprite = adventurer;
				fourthPlace.text = player1Score+" Points " + trophies[1];
			}
			//player2
			if (player2Score == first) {
				firstPlaceSprite.GetComponent<Image>().sprite = wizard;
				firstPlace.text = player2Score+" Points " + trophies[2];
			} else if (player2Score == second) {
				secondPlaceSprite.GetComponent<Image>().sprite = wizard;
				secondPlace.text = player2Score+" Points " + trophies[2];
			} else if (player2Score == third) {
				thirdPlaceSprite.GetComponent<Image>().sprite = wizard;
				thirdPlace.text = player2Score+" Points " + trophies[2];
			} else {
				fourthPlaceSprite.GetComponent<Image>().sprite = wizard;
				fourthPlace.text = player2Score+" Points " + trophies[2];
			}
			//player3
			if (player3Score == first) {
				firstPlaceSprite.GetComponent<Image>().sprite = knight;
				firstPlace.text = player3Score+" Points " + trophies[3];
			} else if (player3Score == second) {
				secondPlaceSprite.GetComponent<Image>().sprite = knight;
				secondPlace.text = player3Score+" Points " + trophies[3];
			} else if (player3Score == third) {
				thirdPlaceSprite.GetComponent<Image>().sprite = knight;
				thirdPlace.text = player3Score+" Points " + trophies[3];
			} else {
				fourthPlaceSprite.GetComponent<Image>().sprite = knight;
				fourthPlace.text = player3Score+" Points " + trophies[3];
			}
			//player4
			if (player4Score == first) {
				firstPlaceSprite.GetComponent<Image>().sprite = pirate;
				firstPlace.text = player4Score+" Points " + trophies[4];
			} else if (player4Score == second) {
				secondPlaceSprite.GetComponent<Image>().sprite = pirate;
				secondPlace.text = player4Score+" Points " + trophies[4];
			} else if (player4Score == third) {
				thirdPlaceSprite.GetComponent<Image>().sprite = pirate;
				thirdPlace.text = player4Score+" Points " + trophies[4];
			} else {
				fourthPlaceSprite.GetComponent<Image>().sprite = pirate;
				fourthPlace.text = player4Score+" Points " + trophies[4];
			}
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
			trophies [i] = "Hello world";
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
