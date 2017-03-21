using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class ResetGame : MonoBehaviour {
    public GameObject scoreSystem;

    int p1score = 0;
    int p2score = 0;
    int p3score = 0;
    int p4score = 0;

    int tie_for_first = 0;

    List<int> scores = new List<int>();
    int firstPlaceScore = 0;
    int secondPlaceScore = 0;
    int thirdPlaceScore = 0;
    int fourthPlaceScore = 0;

    int player1Placement = 1;
    int player2Placement = 1;
    int player3Placement = 1;
    int player4Placement = 1;
    
    public Text FirstPlaceText;
    public Text SecondPlaceText;
    public Text ThirdPlaceText;
    public Text ForthPlaceText;

    public GameObject Reset;
    public GameObject MainMenu;

    public GameObject Player1Icon;
    public GameObject Player2Icon;
    public GameObject Player3Icon;
    public GameObject Player4Icon;

    bool reset_selected;
    bool main_menu_selected;

    public Sprite Reset_selected_sprite;
    public Sprite Reset_deselected_sprite;
    public Sprite MainMenu_selected_sprite;
    public Sprite MainMenu_deselected_sprite;

    public Transform[] Positions = new Transform[8];



    // Use this for initialization
    void Start () {

        //Some score system reset code
        scoreSystem = ScoreSystem.Instance.GetComponent<GameObject>();
        GameOptions.levelsCompleted = 0;

        for(int i = 0; i < GameOptions.maxLevels; i++)
        {
            GameOptions.levelsPlayed[i] = false;
        }
//Seting up the scores=====================================
		p1score = ScoreSystem.Instance.player [1].GetScore ();
		p2score = ScoreSystem.Instance.player [2].GetScore ();
		p3score = ScoreSystem.Instance.player [3].GetScore ();
		p4score = ScoreSystem.Instance.player [4].GetScore ();


        scores.Add(p1score);
        scores.Add(p2score);
        scores.Add(p3score);
        scores.Add(p4score);

        firstPlaceScore = ScoreSetup();
        secondPlaceScore = ScoreSetup();
        thirdPlaceScore = ScoreSetup();
        fourthPlaceScore = ScoreSetup();
//end

//============================Figuring out placement===================
    /*
     * 
     */
        if (p1score == firstPlaceScore)
            player1Placement = 1;
        else if (p1score == secondPlaceScore)
            player1Placement = 2;
        else if (p1score == thirdPlaceScore)
            player1Placement = 3;
        else
            player1Placement = 4;

        if (p2score == firstPlaceScore)
            player2Placement = 1;
        else if (p2score == secondPlaceScore)
            player2Placement = 2;
        else if (p2score == thirdPlaceScore)
            player2Placement = 3;
        else
            player2Placement = 4;

        if (p3score == firstPlaceScore)
            player3Placement = 1;
        else if (p3score == secondPlaceScore)
            player3Placement = 2;
        else if (p3score == thirdPlaceScore)
            player3Placement = 3;
        else
            player3Placement = 4;

        if (p4score == firstPlaceScore)
            player4Placement = 1;
        else if (p4score == secondPlaceScore)
            player4Placement = 2;
        else if (p4score == thirdPlaceScore)
            player4Placement = 3;
        else
            player4Placement = 4;
//======================end of placement========================

//putting icons in thier correct places==========================
        if (p1score == firstPlaceScore)
            tie_for_first++;

        if (p2score == firstPlaceScore)
            tie_for_first++;

        if (p3score == firstPlaceScore)
            tie_for_first++;

        if (p4score == firstPlaceScore)
            tie_for_first++;

        if (tie_for_first == 1)
        {
            if (player1Placement == 1)
                Player1Icon.transform.position = Positions[1].position;
            else if (player2Placement == 1)
                Player2Icon.transform.position = Positions[1].position;
            else if (player3Placement == 1)
                Player3Icon.transform.position = Positions[1].position;
            else if (player4Placement == 1)
                Player4Icon.transform.position = Positions[1].position;
        }
        else if (tie_for_first == 2)
        {
            if (player1Placement == 1)
                Player1Icon.transform.position = Positions[0].position;
            else if (player2Placement == 1)
                Player2Icon.transform.position = Positions[0].position;
            else if (player3Placement == 1)
                Player3Icon.transform.position = Positions[0].position;
            else if (player4Placement == 1)
                Player4Icon.transform.position = Positions[0].position;

            if (player4Placement == 1)
                Player4Icon.transform.position = Positions[2].position;
            else if (player3Placement == 1)
                Player3Icon.transform.position = Positions[2].position;
            else if (player2Placement == 1)
                Player2Icon.transform.position = Positions[2].position;
            else if (player1Placement == 1)
                Player1Icon.transform.position = Positions[2].position;
        }
        else if (tie_for_first == 3)
        {
            if (player1Placement != 1)
            {
                Player1Icon.transform.position = Positions[0].position;
            }
            else if (player2Placement != 1)
            {
                Player2Icon.transform.position = Positions[0].position;
            }
            else if (player3Placement != 1)
            {
                Player3Icon.transform.position = Positions[0].position;
            }
            else if (player4Placement != 1)
            {
                Player4Icon.transform.position = Positions[0].position;
            }
        }
        else
        {
            //all four tied
        }




//end=======================================================================

        //more reset code
		ScoreSystem.Instance.player [1].SetScore (0);
		ScoreSystem.Instance.player [2].SetScore (0);
		ScoreSystem.Instance.player [3].SetScore (0);
		ScoreSystem.Instance.player [4].SetScore (0);

    }

    void Update ()
    {

        if(Input.GetButtonDown("A_P1"))
        {
            if(reset_selected)
            {
                GameOptions.Instance.LoadLevel();
            }
            else
            {
                SceneManager.LoadScene("GameStart");
            }
        }

        float horzInput = Input.GetAxis("D_PAD");
        float horz2Inupt = Input.GetAxis("Horizontal");

        if(horzInput < -.15 || horz2Inupt < -.15 || horzInput > .15 || horz2Inupt > .15)
        {
            StartCoroutine("ChangeSelected");
        }
    }

    IEnumerator ChangeSelected()
    {
        if(reset_selected)
        {
            reset_selected = false;
            main_menu_selected = true;
            Reset.GetComponent<Image>().sprite = Reset_deselected_sprite;
            MainMenu.GetComponent<Image>().sprite = MainMenu_selected_sprite;
        }
        else
        {
            reset_selected = true;
            main_menu_selected = false;
            Reset.GetComponent<Image>().sprite = Reset_deselected_sprite;
            MainMenu.GetComponent<Image>().sprite = MainMenu_selected_sprite;
        }
        yield return new WaitForSeconds(.15f);
    }

    int ScoreSetup()
    {
        if(scores.Count == 0)
        {
            return -1;
        }
        int score = scores.Max();
        int max = scores.Max();
        while(max == score)
        {
            scores.Remove(scores.Max());
            if (scores.Count == 0)
                max = -1;
            else
                max = scores.Max();
        }
        return score;
    }
}
