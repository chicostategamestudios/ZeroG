using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    public bool On;
    public float OnTimer;
    public float OffTimer;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!On)
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<BoxCollider>().enabled = false;
            Invoke("GateOnTimer", OffTimer);
        }
        else
        {
            
            GetComponent<BoxCollider>().enabled = true;
            Invoke("GateOffTimer", OnTimer);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        On = true;
    }

    void GateOffTimer()
    {
        On = false;
    }

    void GateOnTimer()
    {
        On = true;
    }
}
