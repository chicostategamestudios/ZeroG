using UnityEngine;
using System.Collections;

public class testController : MonoBehaviour {
    public float a;
    public float b;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        a = Input.GetAxis("Horizontal");
        b = Input.GetAxis("Horizontal2");

    }
}
