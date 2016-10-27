using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {

	public static SpawnControl S;
	public bool spawnA;
	public bool spawnB;
	public bool spawnX;
	public bool spawnY;
	// Use this for initialization
	void Start () {
		S=this;
		spawnA = false;
		spawnB = false;
		spawnX = false;
		spawnY = false;
	}
}
