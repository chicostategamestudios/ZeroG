using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
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

	// Use this for initialization
	void Start () {
	
	}

	void CheckScore(){
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
		//player1
		if (player1Score == first && player2Score != first && player3Score != first && player4Score != first) {
			p1.transform.position = p1FirstPlace.transform.position;
		} else if (player1Score == second && player2Score != second && player3Score != second && player4Score != second) {
			p1.transform.position = p1SecondPlace.transform.position;
		} else if (player1Score == third && player2Score != third && player3Score != third && player4Score != third) {
			p1.transform.position = p1ThirdPlace.transform.position;
		} else {
			p1.transform.position = p1FourthPlace.transform.position;
		}
		//player2
		if (player2Score == first && player3Score != first && player4Score != first && player1Score != first) {
			p2.transform.position = p2FirstPlace.transform.position;
		} else if (player2Score == second && player3Score != second && player4Score != second && player1Score != second) {
			p2.transform.position = p2SecondPlace.transform.position;
		} else if (player2Score == third && player3Score != third && player4Score != third && player1Score != third) {
			p2.transform.position = p2ThirdPlace.transform.position;
		} else {
			p2.transform.position = p2FourthPlace.transform.position;
		}
		//player3
		if (player3Score == first && player4Score != first && player1Score != first && player2Score != first) {
			p3.transform.position = p3FirstPlace.transform.position;
		} else if (player3Score == second && player4Score != second && player1Score != second && player2Score != second) {
			p3.transform.position = p3SecondPlace.transform.position;
		} else if (player3Score == third && player4Score != third && player1Score != third && player2Score != third) {
			p3.transform.position = p3ThirdPlace.transform.position;
		} else {
			p3.transform.position = p3FourthPlace.transform.position;
		}
		//player4
		if (player4Score == first && player1Score != first && player2Score != first && player3Score != first) {
			p4.transform.position = p4FirstPlace.transform.position;
		} else if (player4Score == second && player1Score != second && player2Score != second && player3Score != second) {
			p4.transform.position = p4SecondPlace.transform.position;
		} else if (player4Score == third && player1Score != third && player2Score != third && player3Score != third) {
			p4.transform.position = p4ThirdPlace.transform.position;
		} else {
			p4.transform.position = p4FourthPlace.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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
		if (SceneManager.GetActiveScene ().name == "PlayerStanding") {
			CheckScore ();
		}
	}
}
