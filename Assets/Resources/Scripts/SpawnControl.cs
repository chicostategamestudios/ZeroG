using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {

	public static SpawnControl S;
	public bool spawnA;
	public bool spawnB;
	public bool spawnX;
	public bool spawnY;

	int[] aLocation;
	public int[] bLocation;
	public int[] xLocation;
	public int[] yLocation;

	// Use this for initialization
	void Start () {
		S=this;
		spawnA = false;
		spawnB = false;
		spawnX = false;
		spawnY = false;

		aLocation = new int[2];
		bLocation = new int[2];
		xLocation = new int[2];
		yLocation = new int[2];
	}

	public void getA(int aX, int aY){
		aLocation [0] = aX;
		aLocation [1] = aY;
		//Debug.Log ("getting AA");
	}
	public void getB(int bX, int bY){
		bLocation [0] = bX;
		bLocation [1] = bY;
	}
	public void getX(int xX, int xY){
		xLocation [0] = xX;
		xLocation [1] = xY;
	}
	public void getY(int yX, int yY){
		yLocation [0] = yX;
		yLocation [1] = yY;
	}

	public int[] giveA(){
		//Debug.Log (aLocation[0]);
		//Debug.Log (aLocation[1]);
		return aLocation;
	}
	public int[] giveB(){
		return bLocation;
	}
	public int[] giveX(){
		return xLocation;
	}
	public int[] giveY(){
		return yLocation;
	}

}
