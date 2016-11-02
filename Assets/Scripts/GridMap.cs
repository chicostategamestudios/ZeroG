using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine.UI;

public class GridMap : MonoBehaviour {


    public Texture2D map; //must be RW enabled and a TGA file
    public bool generateArray = false;
    public GameObject smallAsteroid;
    public GameObject largeAsteroid;
	public GameObject xSpawn;
	public GameObject bSpawn;
	public GameObject ySpawn;
	public GameObject aSpawn;
	public GameObject goal;
	public GameObject spaceMine;
	public GameObject movingAsteroid;

	private List<GameObject> asteroids = new List<GameObject>();
	private List<MeshRenderer> asteroidRenders = new List<MeshRenderer>();

	//public StudioEventEmitter music;
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
	private float timer = 6f;
	//private Component[] render;
	private int numPlayers;
	private GameObject player;
	private bool findingPlayers;
	private int playerNum;
	//private GameObject level;
	private bool gameStart;
	private bool playing;
	//private bool playMusic;

    // Use this for initialization
    void Start () {
		//playMusic = true;
		playing = false;
		lights.SetActive (false);
		gameStart = false;
		text.text = "";
		findingPlayers = true;
		playerNum = 1;
		myRed = red.GetComponent<SpriteRenderer>();
		myOrange = orange.GetComponent<SpriteRenderer>();
		myYellow = yellow.GetComponent<SpriteRenderer>();
		myGreen = green.GetComponent<SpriteRenderer>();
		generateArray = true;
	}

	IEnumerator GameStart(){
		yield return new WaitForSeconds(startTimer);
		foreach(MeshRenderer rend in asteroidRenders){
			rend.enabled = false;
			Debug.Log("rend off");
		}
		text.text = "Choose Your Launch Pad!";
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

	/*void Music(){
		if (playMusic) {
			music.Play ();
		}
		playMusic = false;
	}*/

	void GetRenders(){
		foreach(GameObject go in asteroids){
			MeshRenderer render = go.GetComponent<MeshRenderer>();
			asteroidRenders.Add(render);
		}
		StartCoroutine("GameStart");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(generateArray == true)
        {
            int xSize = map.width;
            int ySize = map.height;

            Vector2[] mapObjPos = new Vector2[xSize * ySize];
            int[] mapObjs = new int[xSize * ySize];

            Debug.Log(xSize);

            for (int horrizontalPixels = 0; horrizontalPixels < xSize; horrizontalPixels++)
            {
                for (int verticalPixels = 0; verticalPixels < ySize; verticalPixels++)
                {
                    if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.red)
                    {

                        //  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

                        Instantiate(bSpawn, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.Euler(90, 0, 0));
                    }
                    else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.blue)
                    {
                        //  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

                        Instantiate(xSpawn, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.Euler(90, 0, 0));
                    }
                    else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.green)
                    {
                        //  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

                        Instantiate(aSpawn, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.Euler(-90, 0, 0));
                    }
                    else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.yellow)
                    {
                        //  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

                        Instantiate(ySpawn, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.Euler(90, 0, 0));
                    }
                    else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.magenta)
                    {
                        //  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

                        Instantiate(goal, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.identity);
                    }
					else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.black)
					{
						//  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;
						GameObject sa;
						sa = Instantiate(smallAsteroid, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.identity) as GameObject;
						asteroids.Add(sa);
					}
					else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.grey)
					{
						//  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;
						GameObject la;
						la = Instantiate(largeAsteroid, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.identity) as GameObject;
						asteroids.Add(la);
					}
					else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.magenta)
					{
						//  mapObjs[horrizontalPixels * ySize + verticalPixels] = 1;

						Instantiate(spaceMine, new Vector3(horrizontalPixels, 0, verticalPixels), Quaternion.identity);
					}
					else if (map.GetPixel(horrizontalPixels, verticalPixels) == Color.white)
					{

					}

                    Debug.Log(map.GetPixel(horrizontalPixels, verticalPixels));
                
                    // mapObjPos[horrizontalPixels * ySize + verticalPixels] = new Vector2(horrizontalPixels, verticalPixels);
                }
            }





            generateArray = false;
			GetRenders();
        }//end map generation

		if (findingPlayers) {
			FindPlayers ();
		}

		if (!playing) {
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
							foreach(MeshRenderer rend in asteroidRenders){
								rend.enabled = true;
							}
							playing = true;
						}
					}
				}

				if (numPlayers == 3) {
					if (PlayerController.S.playerReady && PlayerController2.S.playerReady && PlayerController3.S.playerReady) {
						//Music();
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
							foreach(MeshRenderer rend in asteroidRenders){
								rend.enabled = true;
							}
							playing = true;
						}
					}
				}

				if (numPlayers == 4) {
					if (PlayerController.S.playerReady && PlayerController2.S.playerReady && PlayerController3.S.playerReady && PlayerController4.S.playerReady) {
						//Music();
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
							foreach(MeshRenderer rend in asteroidRenders){
								rend.enabled = true;
							}
							playing = true;
						}
					}
				}
			}
		}
	
	}//end Update
}
