using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour {

    public static GameOptions Instance;
    public static bool player1;
    public static bool player2;
    public static bool player3;
    public static bool player4;
    public static int numOfRounds;
    public static bool[] levelsPlayed;
    public static int maxLevels;
    public static int levelsCompleted;

	Image p1Icon;
	Image p2Icon;
	Image p3Icon;
	Image p4Icon;
	bool iconsDealt = true;

    // Use this for initialization

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadOptions(bool p1, bool p2, bool p3, bool p4, int numRounds, int max)
    {
        player1 = p1;
        player2 = p2;
        player3 = p3;
        player4 = p4;
        numOfRounds = numRounds;
        maxLevels = max;
        levelsCompleted = 0;

        levelsPlayed = new bool[maxLevels];
        for (int i = 0; i < maxLevels; i++)
        {
            levelsPlayed[i] = false;
        }
            
    }

    public void LoadLevel()
    {
        bool found = false;
        int randomNumber = 0;
        if (levelsCompleted != numOfRounds)
        {
            while (!found)
            {
                randomNumber = Random.Range(3, maxLevels + 1);
                if (!levelsPlayed[randomNumber - 1])
                {
                    //string levelName = "Level_" + randomNumber;
                    //print(levelName);
                    levelsPlayed[randomNumber - 1] = true;
					SceneManager.LoadScene(randomNumber);
					//SceneManager.LoadScene("practiceLevel");

                    found = true;
                }

            }
            levelsCompleted++;
			ScoreSystem.Instance.current_level += 1;
			//Give each player a space to store their stats in for this level
			for (int i = 1; i <= 4; i++) {
				ScoreSystem.Instance.player [i].AddLevel (ScoreSystem.Instance.current_level);
			}
        } else
        {
            SceneManager.LoadScene("Victory");
        }
		//SceneManager.LoadScene ("practice");
    }

	public void Update(){
		string sceneName = SceneManager.GetActiveScene ().name;
		if(sceneName == "Victory"){
			if(Input.GetButtonDown("A_P1")){
				levelsCompleted = 0;
				ScoreSystem.Instance.player [1].SetScore (0);
				ScoreSystem.Instance.player [2].SetScore (0);
				ScoreSystem.Instance.player [3].SetScore (0);
				ScoreSystem.Instance.player [4].SetScore (0);
				LoadLevel();
			}

			if(Input.GetButtonDown("B_P1")){
				SceneManager.LoadScene("GameStart");
			}
		} else if(sceneName != "GameStart"){
			GameObject tmp;
			tmp = GameObject.FindGameObjectWithTag ("exploIcon");
			p1Icon = tmp.GetComponent<Image> ();

			tmp = GameObject.FindGameObjectWithTag ("wizIcon");
			p2Icon = tmp.GetComponent<Image> ();

			tmp = GameObject.FindGameObjectWithTag ("kniIcon");
			p3Icon = tmp.GetComponent<Image> ();

			tmp = GameObject.FindGameObjectWithTag ("pirIcon");
			p4Icon = tmp.GetComponent<Image> ();

			if (!player1) {
				p1Icon.enabled = false;
			}
			if (!player2) {
				p2Icon.enabled = false;
			}
			if (!player3) {
				p3Icon.enabled = false;
			}
			if (!player4) {
				p4Icon.enabled = false;
			}
		}
	}

	public int GetRound(){
		return levelsCompleted;
	}

	public int GetTotalRounds(){
		return numOfRounds;
	}
}
