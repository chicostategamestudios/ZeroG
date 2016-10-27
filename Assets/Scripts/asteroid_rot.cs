using UnityEngine;
using System.Collections;

public class asteroid_rot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate((Vector3.right * Time.deltaTime) * 2);

        transform.Rotate((Vector3.up * Time.deltaTime) * 2, Space.World);

    }
}
