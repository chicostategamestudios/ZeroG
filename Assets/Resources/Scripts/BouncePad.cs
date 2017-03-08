//Zero G - Created by:Thaddeus Thompson 2/9/2017 - Last Modified: Thaddeus Thompson 2/9/2017

using UnityEngine;
using System.Collections;

public class BouncePad : MonoBehaviour {

	public int direction = 0;
	public int lastDir = 3;
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
			direction++;
			Rotate ();
		}
	}

	//Chooses a random number and compares to determine rotation
	//Can change to cycle through direction in specific order
	void Rotate(){
		//direction = Random.Range (1, 5);
		//face right
		if(direction == 2){
			transform.rotation = Quaternion.Euler(0,90,0);
			timer = reset;
			lastDir = 1;
		}
		//face left
		if(direction == 4){
			transform.rotation = Quaternion.Euler(0,270,0);
			timer = reset;
			lastDir = 2;
			direction = 0;
		}
		//face up
		if(direction == 1){
			transform.rotation = Quaternion.Euler(0,0,0);
			timer = reset;
			lastDir = 3;
		}
		//face down
		if(direction == 3){
			transform.rotation = Quaternion.Euler(0,180,0);
			timer = reset;
			lastDir = 4;
		}
	}

	//get face direction and return it to player contoller
	public int GetDirection(){
		//Vector3 rotation = transform.rotation;
		return lastDir;
	}
		
	public void GetListing(int count, int xPos, int yPos, GameObject m){
		num = count;
		posX = xPos;
		posY = yPos;
		map = m.GetComponent<GridMap> ();
	}
		
}
