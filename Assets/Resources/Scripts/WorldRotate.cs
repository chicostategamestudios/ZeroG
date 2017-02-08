using UnityEngine;
using System.Collections;

public class WorldRotate : MonoBehaviour {

	public float speed;
	public GameObject space;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * (speed*Time.deltaTime));
		space.transform.Rotate (Vector3.up * (speed * Time.deltaTime));
	}
}
