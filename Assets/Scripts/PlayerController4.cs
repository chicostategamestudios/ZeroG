using UnityEngine;
using System.Collections;

public class PlayerController4 : MonoBehaviour {

	public static PlayerController4 S;

	private bool stopped = true;
	private float movementSpeed = 17f;
	private GameObject spawnPoint;
	private float speedTimer;
	private float coolDown = 0.13f;
	private float speedUp;
	private float speedMult = 5.8f;
	private float maxSpeed = 18.3f;
	private float lastDirection;

	public bool inMenu;
	public bool playerReady;

	private bool hitAsteroid;
	private float length = 1f;
	private float yUp = 0.5f;

	private ParticleSystem myParticle;
	private CharacterController charCont;
	private int layerMask = 1 << 8;
	private Vector3 up;
	private Vector3 forward;

	// Use this for initialization
	void Start () {
		up = new Vector3(0,yUp,0);
		S = this;
		inMenu = true;
		playerReady = true;
		myParticle = GetComponentInChildren<ParticleSystem> ();
		charCont = GetComponent<CharacterController> ();
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		forward = this.transform.TransformDirection (Vector3.forward);
		/*Vector3 up = new Vector3 (0, yUp, 0);
		Vector3 forward = transform.TransformDirection (Vector3.forward) * length;
		Debug.DrawRay (transform.position + up, forward, Color.green, 100);*/
		GetInput();
		if(speedTimer > 0){
			speedTimer -= Time.deltaTime;
		}
		if(speedTimer < 0){
			speedTimer = 0;
		}
        //Gets player input for spawn location
        //Use  Spawn_XYAB(Clone) becuase that is how Unity decides to name them when instantiating
        if (inMenu && playerReady == false) {
			if(Input.GetButtonDown("A_P4") && SpawnControl.S.spawnA == false){
				spawnPoint = GameObject.Find("Spawn_A(Clone)");
				SpawnControl.S.spawnA = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("B_P4") && SpawnControl.S.spawnB == false){
				spawnPoint = GameObject.Find("Spawn_B(Clone)");
				SpawnControl.S.spawnB = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("X_P4") && SpawnControl.S.spawnX == false){
				spawnPoint = GameObject.Find("Spawn_X(Clone)");
				SpawnControl.S.spawnX = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("Y_P4") && SpawnControl.S.spawnY == false){
				spawnPoint = GameObject.Find("Spawn_Y(Clone)");
				SpawnControl.S.spawnY = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
		}

		//Check if game has started and if player is stopped
		if (stopped == true && inMenu == false) {
			speedUp = 0;
			//up
			if (lastDirection == 3) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
			//right
			if (lastDirection == 1) {
				transform.rotation = Quaternion.Euler (0, 90, 0);
			}
			//down
			if (lastDirection == 4) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
			//left
			if (lastDirection == 2) {
				transform.rotation = Quaternion.Euler (0, -90, 0);
			}

			//If not facing asteroid player starts moving
			if (Input.GetButton("A_P4") && hitAsteroid == false) {
				stopped = false;
			}

		}
        //=============================Collision System ==================================   
        //3 raycast one projects from the back left corner 
        //One from center back 
        //one from right back corner of the player 
        //(for each direction)

        //if any of them detect an astroid
        //then find the raycast that has the shortest distance between point and end of raycast
        //find the point on the raycast, subtract the distance between that point
        //and the end of the raycast (and the orgin to the end of the collider) and
        //move that distance.
        //Else, just move the full distance

        RaycastHit hit;
        float shortest_distance = 0f;
        int rays_hit = 0;
        //==========================UP===========================
        if (lastDirection == 3)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(transform.position, hit.point);
                //if a raycast hits, then record if it's the shortest distance
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }

                //Add one to ray hits if it hits an astriod (This will be used to tell the ship to stop later)
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x - .75f, transform.position.y, transform.position.z),
                Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x - .75f, transform.position.y, transform.position.z), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x + .95f, transform.position.y, transform.position.z),
                Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x + .95f, transform.position.y, transform.position.z), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
        }

        //==========================DOWN===========================
        if (lastDirection == 4)
        {
            if (Physics.Raycast(transform.position, -Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(transform.position, hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x + .75f, transform.position.y, transform.position.z),
                -Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x + .75f, transform.position.y, transform.position.z), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x - .95f, transform.position.y, transform.position.z),
                -Vector3.forward, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x - .95f, transform.position.y, transform.position.z), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
        }

        //==========================LEFT===========================
        if (lastDirection == 2)
        {
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 2f))
            {
                Debug.DrawLine(transform.position, hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .75f),
                Vector3.left, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z - .75f), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .95f),
                Vector3.left, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z + .95f), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
        }

        //==========================RIGHT===========================
        if (lastDirection == 1)
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 2f))
            {
                Debug.DrawLine(transform.position, hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .75f),
                Vector3.right, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z + .75f), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .95f),
                Vector3.right, out hit, 2f))
            {
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z - .95f), hit.point);
                if (shortest_distance > hit.distance)
                {
                    Debug.Log("Hit");
                    shortest_distance = hit.distance;
                }
                if (hit.transform.tag == "asteroid")
                {
                    rays_hit++;
                }
            }
        }

        //if there is a hit, then don't move the ship (And don't move the ship, if there happens to be a hit)
        if (rays_hit != 0)
        {
            stopped = true;
            hitAsteroid = true;
        }
        else
        {
            hitAsteroid = false;
        }

        //if the ship is more than it's own body away from an object, than move closer before stopping
        if (shortest_distance > 1)
        {
            charCont.Move(transform.forward * Time.deltaTime * (shortest_distance - 1));
        }

        //=============================END OF COLLISION==============================

        //If player is moving set speed and increase as time passes
        if (stopped == false) {
			if (speedTimer == 0 && speedUp < maxSpeed) {
				speedUp += speedMult;
				speedTimer = coolDown;
			}
			//transform.position += transform.forward * Time.deltaTime * (movementSpeed + speedUp);

			charCont.Move (transform.forward * Time.deltaTime * (movementSpeed + speedUp));
		}

		//Manual respawn
		if (playerReady) {
			if(Input.GetButtonDown("X_P4")){
				myParticle.Play ();
				this.transform.position = spawnPoint.transform.position;
				stopped = true;
			}
		}


	}

	void GetInput() //gets input for playerDirection
	{
        if(stopped) { 
		    float horzInput = Input.GetAxis ("Horizontal4");
		    float vertInput = Input.GetAxis("Vertical4");


		    if (Mathf.Abs (horzInput) > 0.15f) {
			    if (horzInput > 0) {
				    lastDirection = 1f;
			    }
			    if (horzInput < 0) {
				    lastDirection = 2f;
			    }
		    }

		    if (Mathf.Abs (vertInput) > 0.15f) {
			    if (vertInput > 0) {
				    lastDirection = 3f;
			    }
			    if (vertInput < 0) {
				    lastDirection = 4f;
			    }
		    }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        /*  if (col.gameObject.tag == "asteroid")
          {
              if(Physics.Raycast (transform.position + up, forward, length, layerMask)){
                  stopped = true;
                  hitAsteroid = true;
              }else{
                  hitAsteroid = false;
                  stopped = false;
              }
          } */

        if (col.transform.tag == "wall")
        {
            CameraShake.S.shakeDuration = .5f;
            myParticle.Play();
            this.transform.position = spawnPoint.transform.position;
            stopped = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //hitAsteroid = false;
    }

    void OnTriggerStay(Collider col)
    {
        /*if (col.gameObject.tag == "asteroid") {
			
			if(Physics.Raycast (transform.position + up, forward, length, layerMask)){
				stopped = true;
				hitAsteroid = true;
			}else{
				hitAsteroid = false;
			}
		}*/
        if (col.gameObject.tag == "goal")
        {
            stopped = true;
            hitAsteroid = true;

        }
    }

}
