using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    bool hasPressedStart = false;
    int numOfRounds;
    int randomNum;
    public GameObject StartScreen;
    public GameObject OptionsScreen;
    public GameObject player1Ready;
    public GameObject player1Start;
    public GameObject player2Ready;
    public GameObject player2Start;
    public GameObject player3Ready;
    public GameObject player3Start;
    public GameObject player4Ready;
    public GameObject player4Start;
    public GameObject ScoreSystem;
    public GameObject StartGame;
    public GameObject loadingText;
    public Text NumberOfRoundsText;
    bool hasStartedLeft = false;
    bool hasStartedRight = false;
    public int maxNumberOfLevels;
    bool[] levelsPlayed;
    bool gameStarted = false;
    bool isPlayer1Ready = false;
    bool isPlayer2Ready = false;
    bool isPlayer3Ready = false;
    bool isPlayer4Ready = false;


    // Use this for initialization
    void Start () {
        numOfRounds = maxNumberOfLevels;
        setText();
        loadingText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        //if the game has not started
        if (!gameStarted)
        {
            //If on the start screen, and player presses start; Then move to options screen
            if (Input.GetButtonDown("Start") && !hasPressedStart)
            {
                hasPressedStart = true;
                StartScreen.SetActive(false);
                OptionsScreen.SetActive(true);
            }

            //If on the options screen
            if (hasPressedStart)
            {
                //If the dpad or the joystick is going left, than subtract
                float horzInput = Input.GetAxis("D_PAD");
                float horz2Inupt = Input.GetAxis("Horizontal");


                //========Subtracting one from rounds played=============
                //will subtract one from rounds played as long as player 1
                //is pressing left on the joystick or dpad
                if (horzInput < -.15 || horz2Inupt < -.15)
                {
                    if (!hasStartedLeft)
                    {
                        hasStartedLeft = true;
                        StartCoroutine("GoLeft");
                    }
                }
                else
                {
                    if (hasStartedLeft)
                    {
                        StopCoroutine("GoLeft");
                        hasStartedLeft = false;
                    }
                }
                //========Adding one from rounds played=============
                //will add one from rounds played as long as player 1
                //is pressing right on the joystick or dpad
                if (horzInput > .15 || horz2Inupt > .15)
                {
                    if (!hasStartedRight)
                    {
                        hasStartedRight = true;
                        StartCoroutine("GoRight");
                    }
                }
                else
                {
                    if (hasStartedRight)
                    {
                        StopCoroutine("GoRight");
                        hasStartedRight = false;
                    }
                }

                //=============Ready up===============

                //If player1 is not ready
                //and he presses start; Ready player one
                //If player1 already pressed start, then start the game
                if (!isPlayer1Ready)
                {
                    if (Input.GetButtonDown("Start"))
                    {
                        isPlayer1Ready = true;
                        player1Start.SetActive(false);
                        player1Ready.SetActive(true);
                        StartGame.SetActive(true);
                    }
                }
                else
                {
                    if(Input.GetButtonDown("Start"))
                    {
                        startGame();
                        loadingText.SetActive(true);
                    }
                }

                //If player2 is not ready
                //and they presses start Ready player 2
                //if player 2 already pressed start, then reset them
                if (!isPlayer2Ready)
                {
                    if (Input.GetButtonDown("Start_P2"))
                    {
                        isPlayer2Ready = true;
                        player2Start.SetActive(false);
                        player2Ready.SetActive(true);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Start_P2"))
                    {
                        isPlayer2Ready = false;
                        player2Ready.SetActive(false);
                        player2Start.SetActive(true);
                    }
                }

                //If player3 is not ready
                //and they presses start Ready player 3
                //if player 3 already pressed start, then reset them
                if (!isPlayer3Ready)
                {
                    if (Input.GetButtonDown("Start_P3"))
                    {
                        isPlayer3Ready = true;
                        player3Start.SetActive(false);
                        player3Ready.SetActive(true);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Start_P3"))
                    {
                        isPlayer3Ready = false;
                        player3Ready.SetActive(false);
                        player3Start.SetActive(true);
                    }
                }

                //If player4 is not ready
                //and they presses start Ready player 4
                //if player 4 already pressed start, then reset them
                if (!isPlayer4Ready)
                {
                    if (Input.GetButtonDown("Start_P4"))
                    {
                        isPlayer4Ready = true;
                        player4Start.SetActive(false);
                        player4Ready.SetActive(true);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Start_P4"))
                    {
                        isPlayer4Ready = false;
                        player4Ready.SetActive(false);
                        player4Start.SetActive(true);
                    }
                }

            }
        }
	}

    IEnumerator GoLeft()
    {
        //subtract a number, if it's too low, than go back to the top
        if(numOfRounds == 1)
        {
            numOfRounds = maxNumberOfLevels;
        } else
        {
            numOfRounds--;
        }
        setText();
        yield return new WaitForSeconds(.15f);
    }

    IEnumerator GoRight()
    {
        //add a number, if it's too high then start back at the beginning
        if (numOfRounds == maxNumberOfLevels)
        {
            numOfRounds = 1;
        }
        else
        {
            numOfRounds++;
        }
        setText();
        yield return new WaitForSeconds(.15f);
    }

    void setText()
    {
        NumberOfRoundsText.text = numOfRounds.ToString();
    }

    void startGame()
    {
        //Store varibles in a permanent structure and then load a random level.
        ScoreSystem.GetComponent<GameOptions>().LoadOptions(isPlayer1Ready, isPlayer2Ready, 
            isPlayer3Ready, isPlayer4Ready, numOfRounds, maxNumberOfLevels);
        ScoreSystem.GetComponent<GameOptions>().LoadLevel();
    }
}
