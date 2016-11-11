using UnityEngine;
using System.Collections;

public class Moving_Wall : MonoBehaviour {

    public Transform[] movePoints;
    private int currentMovePoint;
    public float moveSpeed;


	// Use this for initialization
	void Start () {
        transform.position = movePoints[0].position;
        currentMovePoint = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position == movePoints[currentMovePoint].position)
        {
            currentMovePoint++;
        }

        if (currentMovePoint >= movePoints.Length)
        {
            currentMovePoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentMovePoint].position, moveSpeed * Time.deltaTime);
	}
}