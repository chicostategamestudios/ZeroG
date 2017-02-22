
// Script Written by Truro Hawkins  | Last Edited by Truro Hawkins | Last Modified 2/1/17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// only needed for testing the quit button in editor using UnityEditor;

public class MenuSelector : MonoBehaviour {

    [Tooltip("Image of button with bright red border")]
    public Sprite active_button;
    [Tooltip("Image of button with greyed out border")]
    public Sprite inactive_button;
    [Tooltip("size = 3 names of scene; 0 - Start, 1 - Options, 2 - Credits")]
    public string[] levels;
    [Tooltip("SpriteRenderers from children; size = 4;  0 - Start, 1 - Options, 2 - Credits, 3 - Quit")]
	public GameObject[] buttons;
    [Tooltip("Text also from children; size = 4; 0 - Start, 1 - Options, 2 - Credits, 3 - Quit")]
    public Text[] butt_texts;

    Color inactive_text; // text color for inactive buttons
    Color active_text; // text color for the active button
    int selector = 0; // indexes arrays
    bool limit_movement = false; // keeps movement of dpad and controller within reason

	GameStart starter;

    void Start () {
        active_text = new Color(1, 0, 0, 1);
        inactive_text = butt_texts[selector].color;
        butt_texts[selector].color = active_text;
		buttons[selector].GetComponent<Image>().sprite = active_button;
		GameObject tmp = GameObject.Find ("GameStartCanvas");
		starter = tmp.GetComponent<GameStart> ();
    }
	
	void Update () {
        Controls();
	}


    /* keyboard support
     * Xbox controller support Analog & dpad
     * names of axises may need to be changed to fit input manager for main branch
     * current names: Horizontal; dPad; Submit
     **/
    void Controls()
    {
        if ((Input.GetAxis("Horizontal") == -1 || Input.GetAxis("D_PAD") == -1) && !limit_movement)
        {

            MoveBack();
            limit_movement = true;

        } else if((Input.GetAxis("Horizontal") == 1 || Input.GetAxis("D_PAD") == 1) && !limit_movement)
        {

            MoveForward();
            limit_movement = true;

        } else if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.3 && Input.GetAxis("D_PAD") == 0)
        {
            limit_movement = false;
        }



        if (Input.GetButtonDown("A_P1"))
        {
			if (selector == 0) {
				starter.SetupGame ();
				this.gameObject.SetActive (false);
			}/* else if (selector < 3) {
				SceneManager.LoadScene (levels [selector]);
			}*/
			else if(selector == 3)
			{
				//EditorApplication.isPlaying = false;
				Application.Quit();
			}
        }


        // keyboard support; ok to delete

        if (Input.GetKeyUp("d"))
        {
            MoveForward();
        }

        if (Input.GetKeyUp("a"))
        {
            MoveBack();
        }


        if (Input.GetKeyUp("space"))
        {
			if (selector == 0) {
				starter.SetupGame ();
                this.gameObject.SetActive(false);
            } /*else if (selector < 3) {
				SceneManager.LoadScene (levels [selector]);
			}*/
            else
            {
				//EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
    }

    //increments selector forward, changes text color and sprites
    void MoveForward()
    {
		buttons[selector].GetComponent<Image>().sprite = inactive_button;
        butt_texts[selector].color = inactive_text;
        if (selector < 3)
        {
            selector++;
        }
        else
        {
            selector = 0;
        }
		buttons[selector].GetComponent<Image>().sprite = active_button;
        butt_texts[selector].color = active_text;
    }

    //increments selector backward, changes text color and sprites
    void MoveBack()
    {
        butt_texts[selector].color = inactive_text;
		buttons[selector].GetComponent<Image>().sprite = inactive_button;
        if (selector > 0)
        {
            selector--;
        }
        else
        {
            selector = 3;
        }
		buttons[selector].GetComponent<Image>().sprite = active_button;
        butt_texts[selector].color = active_text;
    }
}
