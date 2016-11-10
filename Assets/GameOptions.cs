using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        if (levelsCompleted != maxLevels)
        {
            while (!found)
            {
                randomNumber = Random.Range(1, maxLevels + 1);
                print(randomNumber);
                print(levelsPlayed[0]);
                if (!levelsPlayed[randomNumber - 1])
                {
                    string levelName = "Level_" + randomNumber;
                    print(levelName);
                    levelsPlayed[randomNumber - 1] = true;
                    SceneManager.LoadScene(levelName);
                    found = true;
                }
            }
            levelsCompleted++;
        } else
        {
            SceneManager.LoadScene("GameStart");
        }
    }
}
