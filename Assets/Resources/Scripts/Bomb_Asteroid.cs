using UnityEngine;
using System.Collections;

public class Bomb_Asteroid : MonoBehaviour {

    public bool exploded;
    public float reformTimer;
    public ParticleSystem part;
    public bool invoked;
    public Light mylight;
    public bool lighton;

	// Use this for initialization
	void Start () {

        part = GetComponent<ParticleSystem>();
        mylight = GetComponent<Light>();
        exploded = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (part.time <= 2)
        {
            part.startSpeed = 2;
            part.startLifetime = .75f;
        }

        if (part.time > 2)
        {
            part.startSpeed = -6;
            part.startLifetime = .25f;
        }


        if (!exploded)
        {
            mylight.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }else if (exploded)
        {
            lighton = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            if (!invoked)
            {
                mylight.enabled = false;
                Invoke("ResetReformTimer", reformTimer);
                invoked = true;
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        part.Play();
        exploded = true;
    }

    void ResetReformTimer()
    {
        invoked = false;
        exploded = false;
    }
}
