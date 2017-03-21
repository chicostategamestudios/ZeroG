using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    public Text textField;
    //public Text score;
    public Text endGameTimer;

   // public Image black;
    private bool p1Win;
    private bool p2Win;
    private bool p3Win;
    private bool p4Win;
    private bool secondPlace = false;
    private bool p1Sec;
    private bool p2Sec;
    private bool p3Sec;
    private bool p4Sec;
    public float endTime;
    public string nextLevel;
    private float levelTimer = 6f;
    private bool nextGame;
    private bool endGame;
    public GameOptions DragScoreSystemHere;
    public GameObject endGameTimerGameObject;
    public TimerDisplay timer;

    // Use this for initialization
    void Start()
    {
        nextGame = false;
        endGame = false;
        textField.text = "";
        //score.text = "";
       // black.enabled = false;
        DragScoreSystemHere = FindObjectOfType<GameOptions>();
    }


	public void Win(int pNum){
		nextGame = true;
		if (endGame == false)
		{

			if (pNum == 0)
			{
				p1Win = true;
				endGame = true;
				ScoreSystem.Instance.player [1].AddScore (5);

				textField.text = "Green Wins!!!";
				//score.text = "Player 1: "+ScoreSystem.Instance.player1Score+" point(s)\nPlayer 2: "+ScoreSystem.Instance.player2Score+" point(s)\nPlayer 3: "+ScoreSystem.Instance.player3Score+" point(s)\nPlayer 4: "+ScoreSystem.Instance.player4Score+" point(s)";
			}

			if (pNum == 1)
			{
				p2Win = true;
				endGame = true;
				ScoreSystem.Instance.player [2].AddScore (5);
				textField.text = "Red Wins!!!";
				//score.text = "Player 1: "+ScoreSystem.Instance.player1Score+" point(s)\nPlayer 2: "+ScoreSystem.Instance.player2Score+" point(s)\nPlayer 3: "+ScoreSystem.Instance.player3Score+" point(s)\nPlayer 4: "+ScoreSystem.Instance.player4Score+" point(s)";
			}

			if (pNum == 2)
			{
				p3Win = true;
				endGame = true;
				ScoreSystem.Instance.player [3].AddScore (5);
				textField.text = "Purple Wins!!!";
				//score.text = "Player 1: "+ScoreSystem.Instance.player1Score+" point(s)\nPlayer 2: "+ScoreSystem.Instance.player2Score+" point(s)\nPlayer 3: "+ScoreSystem.Instance.player3Score+" point(s)\nPlayer 4: "+ScoreSystem.Instance.player4Score+" point(s)";
			}

			if (pNum == 3)
			{
				p4Win = true;
				endGame = true;
				ScoreSystem.Instance.player [4].AddScore (5);
				textField.text = "Blue Wins!!!";
				//score.text = "Player 1: "+ScoreSystem.Instance.player1Score+" point(s)\nPlayer 2: "+ScoreSystem.Instance.player2Score+" point(s)\nPlayer 3: "+ScoreSystem.Instance.player3Score+" point(s)\nPlayer 4: "+ScoreSystem.Instance.player4Score+" point(s)";
			}

			if (endGame)
			{
				timer.startTimer = false;
			}
		}

		if (endGame == true)
		{

			if (pNum == 0 && p1Win == false && secondPlace == false)
			{
				p1Win = true;
				secondPlace = true;
				ScoreSystem.Instance.player [1].AddScore (3);
			}

			if (pNum == 1 && p2Win == false && secondPlace == false)
			{
				p2Win = true;
				secondPlace = true;
				ScoreSystem.Instance.player [2].AddScore (3);
			}

			if (pNum == 2 && p3Win == false && secondPlace == false)
			{
				p3Win = true;
				secondPlace = true;
				ScoreSystem.Instance.player [3].AddScore (3);
			}

			if (pNum == 3 && p4Win == false && secondPlace == false)
			{
				p4Win = true;
				secondPlace = true;
				ScoreSystem.Instance.player [4].AddScore (3);
			}
		}

		if (endGame == true)
		{

			if (pNum == 0 && p1Win == false && secondPlace == true)
			{
				PlayerController.S.StartNewLevel ();
				p1Win = true;
				ScoreSystem.Instance.player [1].AddScore (1);
			}

			if (pNum == 1 && p2Win == false && secondPlace == true)
			{
				PlayerController2.S.StartNewLevel ();
				p2Win = true;
				ScoreSystem.Instance.player [2].AddScore (1);
			}

			if (pNum == 2 && p3Win == false && secondPlace == true)
			{
				PlayerController3.S.StartNewLevel ();
				p3Win = true;
				ScoreSystem.Instance.player [3].AddScore (1);
			}

			if (pNum == 3 && p4Win == false && secondPlace == true)
			{
				PlayerController4.S.StartNewLevel ();
				p4Win = true;
				ScoreSystem.Instance.player [4].AddScore (1);
			}
		}
	}

    void Update()
    {
        if (endGame)
        {
            endTime -= Time.deltaTime;
            if (endTime < 0)
            {
                endGameTimerGameObject.SetActive(false);
                SceneManager.LoadScene("PlayerStanding");
                levelTimer -= Time.deltaTime;
                //black.enabled = true;
                //score.text = "Green: " + ScoreSystem.Instance.player1Score + " points\nRed: " + ScoreSystem.Instance.player2Score + " points\nPurple: " + ScoreSystem.Instance.player3Score + " points\nBlue: " + ScoreSystem.Instance.player4Score + " points";
            }
            if (levelTimer <= 5)
            {
                textField.text = "5";
            }
            if (levelTimer <= 4)
            {
                textField.text = "4";
            }
            if (levelTimer <= 3)
            {
                textField.text = "3";
            }
            if (levelTimer <= 2)
            {
                textField.text = "2";
            }
            if (levelTimer <= 1)
            {
                textField.text = "1";
            }
            if (levelTimer < 0)
            {
                textField.text = "Loading...";
                
            }
            else
            {

                if (endTime > 9)
                {
                    endGameTimerGameObject.SetActive(true);
                    endGameTimer.text = "10";
                }
                else if (endTime > 8)
                {
                    endGameTimer.text = "9";
                }
                else if (endTime > 7)
                {
                    endGameTimer.text = "8";
                }
                else if (endTime > 6)
                {
                    endGameTimer.text = "7";
                }
                else if (endTime > 5)
                {
                    endGameTimer.text = "6";
                }
                else if (endTime > 4)
                {
                    endGameTimer.text = "5";
                }
                else if (endTime > 3)
                {
                    endGameTimer.text = "4";
                }
                else if (endTime > 2)
                {
                    endGameTimer.text = "3";
                }
                else if (endTime > 1)
                {
                    endGameTimer.text = "2";
                }
                else if (endTime > 0)
                {
                    endGameTimer.text = "1";
                }
            }
        }

    }
}
