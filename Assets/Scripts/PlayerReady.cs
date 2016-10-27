using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerReady : MonoBehaviour {

	public Text text;
	public GameObject lights;
	public GameObject red;
	public GameObject orange;
	public GameObject yellow;
	public GameObject green;
	public Sprite redLit;
	public Sprite redDim;
	public Sprite orangeDim;
	public Sprite orangeLit;
	public Sprite yellowDim;
	public Sprite yellowLit;
	public Sprite greenDim;
	public Sprite greenLit;
	[Tooltip("Set time delay for level preview")]public float startTimer;

	private SpriteRenderer myOrange;
	private SpriteRenderer myYellow;
	private SpriteRenderer myGreen;
	private SpriteRenderer myRed;
	private float timer = 5f;
	//private Component[] render;
	private int numPlayers;
	private GameObject player;
	private bool findingPlayers;
	private int playerNum;
	private GameObject level;
	private bool gameStart;
	// Use this for initialization
	void Start () {
		lights.SetActive (false);
		gameStart = false;
		text.text = "";
		level = GameObject.Find ("Asteroids");
		findingPlayers = true;
		playerNum = 1;
		myRed = red.GetComponent<SpriteRenderer>();
		myOrange = orange.GetComponent<SpriteRenderer>();
		myYellow = yellow.GetComponent<SpriteRenderer>();
		myGreen = green.GetComponent<SpriteRenderer>();
		StartCoroutine("GameStart");
	}

	IEnumerator GameStart(){
		yield return new WaitForSeconds(startTimer);
		level.SetActive (false);
		text.text = "Players Choose Your Launch Pad";
		gameStart = true;
		PlayerController.S.playerReady = false;
		PlayerController2.S.playerReady = false;
		PlayerController3.S.playerReady = false;
		PlayerController4.S.playerReady = false;
	}

	void FindPlayers(){
		player = GameObject.FindGameObjectWithTag ("Player"+playerNum);
		if (player != null) {
			numPlayers++;
			playerNum++;
		} else {
			findingPlayers = false;
		}
	}

	// Update is called once per frame
	void Update () {

		if (findingPlayers) {
			FindPlayers ();
		}

		if (gameStart) {
			if (numPlayers == 2) {
				if (PlayerController.S.playerReady && PlayerController2.S.playerReady) {
					text.text = "";
					lights.SetActive (true);
					timer -= Time.deltaTime;
					myRed.sprite = redLit;
					if (timer < 3) {
						myRed.sprite = redDim;
						myOrange.sprite = orangeLit;
					}
					if (timer < 2) {
						myOrange.sprite = orangeDim;
						myYellow.sprite = yellowLit;
					}
					if (timer < 1) {
						myYellow.sprite = yellowDim;
						myGreen.sprite = greenLit;
					}
					if (timer < 0) {
						PlayerController.S.inMenu = false;
						PlayerController2.S.inMenu = false;
						/*render = GetComponentsInChildren<MeshRenderer> ();
						foreach (MeshRenderer rend in render) {
							rend.enabled = false;
						}*/
						myGreen.sprite = greenDim;
						lights.SetActive (false);
						level.SetActive (true);
					}
				}
			}

			if (numPlayers == 3) {
				if (PlayerController.S.playerReady && PlayerController2.S.playerReady && PlayerController3.S.playerReady) {
					text.text = "";
					lights.SetActive (true);
					timer -= Time.deltaTime;
					myRed.sprite = redLit;
					if (timer < 3) {
						myRed.sprite = redDim;
						myOrange.sprite = orangeLit;
					}
					if (timer < 2) {
						myOrange.sprite = orangeDim;
						myYellow.sprite = yellowLit;
					}
					if (timer < 1) {
						myYellow.sprite = yellowDim;
						myGreen.sprite = greenLit;
					}
					if (timer < 0) {
						text.text = "";
						PlayerController.S.inMenu = false;
						PlayerController2.S.inMenu = false;
						PlayerController3.S.inMenu = false;
						/*render = GetComponentsInChildren<MeshRenderer> ();
						foreach (MeshRenderer rend in render) {
							rend.enabled = false;
						}*/
						myGreen.sprite = greenDim;
						lights.SetActive (false);
						level.SetActive (true);
					}
				}
			}

			if (numPlayers == 4) {
				if (PlayerController.S.playerReady && PlayerController2.S.playerReady && PlayerController3.S.playerReady && PlayerController4.S.playerReady) {
					text.text = "";
					lights.SetActive (true);
					timer -= Time.deltaTime;
					myRed.sprite = redLit;
					if (timer < 3) {
						myRed.sprite = redDim;
						myOrange.sprite = orangeLit;
					}
					if (timer < 2) {
						myOrange.sprite = orangeDim;
						myYellow.sprite = yellowLit;
					}
					if (timer < 1) {
						myYellow.sprite = yellowDim;
						myGreen.sprite = greenLit;
					}
					if (timer < 0) {
						text.text = "";
						PlayerController.S.inMenu = false;
						PlayerController2.S.inMenu = false;
						PlayerController3.S.inMenu = false;
						PlayerController4.S.inMenu = false;
						/*render = GetComponentsInChildren<MeshRenderer> ();
						foreach (MeshRenderer rend in render) {
							rend.enabled = false;
						}*/
						myGreen.sprite = greenDim;
						lights.SetActive (false);
						level.SetActive (true);
					}
				}
			}
		}
	}//end update
}
