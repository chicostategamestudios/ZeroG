using UnityEngine;
using System.Collections;

public class Bomb_Asteroid : MonoBehaviour {

    public bool exploded;
    public float reformTimer;

	// Use this for initialization
	void Start () {

        exploded = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!exploded)
        {
            GetComponent<ParticleSystem>().Pause();
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }else
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("ResetReformTimer", reformTimer);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        exploded = true;
    }

    void ResetReformTimer()
    {
        exploded = false;
    }
}
