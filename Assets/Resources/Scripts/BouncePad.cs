//Zero G - Created by:Thaddeus Thompson 2/9/2017 - Last Modified: Thaddeus Thompson 2/9/2017

using UnityEngine;
using System.Collections;

public class BouncePad : MonoBehaviour {

	public int direction;
	public float timer = 10;
	public float reset = 5;

	int num;
	int posX;
	int posY;
	GridMap map;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//Timer countsdown to zero then calls Rotate() method
	void FixedUpdate () {
		timer -= Time.deltaTime;
		if(timer <= 0){
			Rotate ();
		}
	}

	//Chooses a random number and compares to determine rotation
	//Can change to cycle through direction in specific order
	void Rotate(){
		direction = Random.Range (1, 5);
		if(direction == 1){
			transform.rotation = Quaternion.Euler(0,90,0);
			timer = reset;
		}
		if(direction == 2){
			transform.rotation = Quaternion.Euler(0,270,0);
			timer = reset;
		}
		if(direction == 3){
			transform.rotation = Quaternion.Euler(0,0,0);
			timer = reset;
		}
		if(direction == 4){
			transform.rotation = Quaternion.Euler(0,180,0);
			timer = reset;
		}
	}

	public int GetDirection(){
		return direction;
	}

	public void GetListing(int count, int xPos, int yPos, GameObject m){
		num = count;
		posX = xPos;
		posY = yPos;
		map = m.GetComponent<GridMap> ();
	}

	//When the player hits this object Player transform equals this object

	//NOTE: change so don't need if for each tag, test using layer
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player1") {
			Debug.Log ("bounce hit");
			other.gameObject.transform.position = transform.position;
			other.gameObject.transform.rotation = transform.rotation;
		}
	}
}
