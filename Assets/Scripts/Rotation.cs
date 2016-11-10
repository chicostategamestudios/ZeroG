using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
    float RotationSpeed;
    int Direction;
    Vector3 rotate;

    void Start()
    {
        RotationSpeed = Random.Range(1, 250);
        Direction = Random.Range(1, 4);
        
    }

    void Update()
    {
        if(Direction == 1)
            transform.Rotate(Vector3.right * Time.deltaTime * RotationSpeed);
        if (Direction == 2)
            transform.Rotate(Vector3.left * Time.deltaTime * RotationSpeed);
        if (Direction == 3)
            transform.Rotate(Vector3.forward * Time.deltaTime * RotationSpeed);
        if (Direction == 4)
            transform.Rotate(Vector3.back * Time.deltaTime * RotationSpeed);

    }
	


}
