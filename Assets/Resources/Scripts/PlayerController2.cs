using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour {

	public static PlayerController2 S;

	[Tooltip("Sprite Renderer from child")]
	public SpriteRenderer sr;
	[Tooltip("its own meshrenderer")]
	public MeshRenderer mr;

	private bool stopped = true;
	public float movementSpeed = 17f;
	private GameObject spawnPoint;
	private float speedTimer;
	public float coolDown = 0.13f;
	private float speedUp;
	public float speedMult = 5.8f;
	public float maxSpeed = 18.3f;
	private float lastDirection;
	private float inputDirection;

	public bool inMenu;
	public bool playerReady;

	private bool hitAsteroid;
	private float length = 1f;
	private float yUp = 0.5f;

	private ParticleSystem myParticle;
	private CharacterController charCont;
	private BoxCollider myCollider;
	private int layerMask = 1 << 8;
	private Vector3 up;
	private Vector3 forward;
	private float rotateSpeed = 130f;

	private int[] playerPos;
	GridMap map;
	int respawn = 0;
	public int[] respawnPos;
	int x = 0;
	int y = 0;
	bool finished = false;
	Image scoreIcon;


	// Use this for initialization
	void Awake () {
		up = new Vector3(0,yUp,0);
		S = this;
		inMenu = true;
		playerReady = true;
		charCont = GetComponent<CharacterController> ();
		myParticle = GetComponentInChildren<ParticleSystem>();
		myCollider = GetComponent<BoxCollider>();
		playerPos = new int[2];
		respawnPos = new int[2];
		GameObject tmp = GameObject.FindGameObjectWithTag ("wizScore");
		scoreIcon = tmp.GetComponent<Image> ();
		scoreIcon.enabled = false;
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		if (!finished) {
			if ((charCont.collisionFlags & CollisionFlags.Sides) != 0) {
				Debug.Log ("front bash");
				//stopped = true;
			}
			forward = this.transform.TransformDirection (Vector3.forward);
			/*Vector3 up = new Vector3 (0, yUp, 0);
		Vector3 forward = transform.TransformDirection (Vector3.forward) * length;
		Debug.DrawRay (transform.position + up, forward, Color.green, 100);*/
			GetInput ();
			if (speedTimer > 0) {
				speedTimer -= Time.deltaTime;
			}
			if (speedTimer < 0) {
				speedTimer = 0;
			}
			//Gets player input for spawn location
			//Use  Spawn_XYAB(Clone) becuase that is how Unity decides to name them when instantiating
			if (inMenu && playerReady == false) {
				int[] tmp = new int[2];
				if (Input.GetButtonDown ("A_P2") && SpawnControl.S.spawnA == false) {
					//Debug.Log("poo");
					spawnPoint = GameObject.Find ("Spawn_A(Clone)");
					SpawnControl.S.spawnA = true;
					tmp = SpawnControl.S.giveA ();
					playerPos [0] = tmp [0];
					playerPos [1] = tmp [1];
					respawnPos [0] = playerPos [0];
					respawnPos [1] = playerPos [1];
					transform.position = spawnPoint.transform.position;
					spawnPoint.GetComponent<SpriteRenderer> ().enabled = false;
					playerReady = true;

					respawn = 0;
					GameObject gm = GameObject.FindGameObjectWithTag ("Map");
					map = gm.GetComponent<GridMap> ();
				}
				if (Input.GetButtonDown ("B_P2") && SpawnControl.S.spawnB == false) {
					spawnPoint = GameObject.Find ("Spawn_B(Clone)");
					SpawnControl.S.spawnB = true;
					tmp = SpawnControl.S.giveB ();
					playerPos [0] = tmp [0];
					playerPos [1] = tmp [1];
					respawnPos [0] = playerPos [0];
					respawnPos [1] = playerPos [1];
					;
					transform.position = spawnPoint.transform.position;
					spawnPoint.SetActive (false);
					playerReady = true;

					respawn = 1;
					GameObject gm = GameObject.FindGameObjectWithTag ("Map");
					map = gm.GetComponent<GridMap> ();
				}
				if (Input.GetButtonDown ("X_P2") && SpawnControl.S.spawnX == false) {
					spawnPoint = GameObject.Find ("Spawn_X(Clone)");
					SpawnControl.S.spawnX = true;
					tmp = SpawnControl.S.giveX ();
					playerPos [0] = tmp [0];
					playerPos [1] = tmp [1];
					respawnPos [0] = playerPos [0];
					respawnPos [1] = playerPos [1];
					transform.position = spawnPoint.transform.position;
					spawnPoint.SetActive (false);
					playerReady = true;

					respawn = 2;
					GameObject gm = GameObject.FindGameObjectWithTag ("Map");
					map = gm.GetComponent<GridMap> ();
				}
				if (Input.GetButtonDown ("Y_P2") && SpawnControl.S.spawnY == false) {
					spawnPoint = GameObject.Find ("Spawn_Y(Clone)");
					SpawnControl.S.spawnY = true;
					tmp = SpawnControl.S.giveY ();
					playerPos [0] = tmp [0];
					playerPos [1] = tmp [1];
					respawnPos [0] = playerPos [0];
					respawnPos [1] = playerPos [1];
					transform.position = spawnPoint.transform.position;
					spawnPoint.SetActive (false);
					playerReady = true;

					respawn = 3;
					GameObject gm = GameObject.FindGameObjectWithTag ("Map");
					map = gm.GetComponent<GridMap> ();
				}
			}

			if (SceneManager.GetActiveScene ().name == "PlayerStanding") {
				if (inputDirection == 3) {
					transform.Rotate (Vector3.forward * (rotateSpeed * Time.deltaTime));
				} else if (inputDirection == 4) {
					transform.Rotate (Vector3.back * (rotateSpeed * Time.deltaTime));
				}
				inputDirection = 0;
			}

			//Check if game has started and if player is stopped
			if (stopped == true && inMenu == false) {
				speedUp = 0;

				//up
				if (lastDirection == 3) {
					y = 1;
					x = 0;
					transform.rotation = Quaternion.Euler (0, 0, 0);

				}
				//right
				if (lastDirection == 1) {
					y = 0;
					x = 1;
					transform.rotation = Quaternion.Euler (0, 90, 0);

				}
				//down
				if (lastDirection == 4) {
					y = -1;
					x = 0;
					transform.rotation = Quaternion.Euler (0, 180, 0);

				}
				//left
				if (lastDirection == 2) {
					y = 0;
					x = -1;
					transform.rotation = Quaternion.Euler (0, -90, 0);

				}

				//If not facing asteroid player starts moving
				if (Input.GetButton ("A_P2")/* && hitAsteroid == false*/) {
					stopped = false;
				}

			}
			arrayCollision (x, y);

			//Manual respawn
			if (playerReady) {
				if (Input.GetButtonDown ("X_P2")) {
					myParticle.Play ();
					Die ();
					/*this.transform.position = spawnPoint.transform.position;
					stopped = true;*/
				}
			}

		}
	}



	void GetInput() //gets input for playerDirection
	{
		if (stopped)
		{
			float horzInput = Input.GetAxis("Horizontal2");
			float vertInput = Input.GetAxis("Vertical2");


			if (Mathf.Abs(horzInput) > 0.15f)
			{
				if (horzInput > 0)
				{
					lastDirection = 1f;
				}
				if (horzInput < 0)
				{
					lastDirection = 2f;
				}
			}

			if (Mathf.Abs(vertInput) > 0.15f)
			{
				if (vertInput > 0)
				{
					lastDirection = 3f;
					inputDirection = 3f;
				}
				if (vertInput < 0)
				{
					lastDirection = 4f;
					inputDirection = 4f;
				}
			}
		}
	}


	void Die(){
		CameraShake.S.shakeDuration = .5f;
		myParticle.Play();
		this.transform.position = spawnPoint.transform.position;
		stopped = true;
		playerPos [0] = respawnPos [0];
		playerPos [1] = respawnPos [1];

	}

	/** int x and y are the variables for the direction you are moving, betwen -1 and 1 **/

	void arrayCollision(int x, int y){
		if (!stopped) {
			//
			if (playerPos [0] + x >= 0 && playerPos [0] + x < map.getWidth ()) {
				if (playerPos [1] + y >= 0 && playerPos [1] + y < map.getHeight ()) {
					int check = map.getPos (playerPos [0] + x, playerPos [1] + y);
					if (check / 100 == 0) { // empty space
						playerPos [0] += x;
						playerPos [1] += y;
						charCont.Move (transform.forward);
						//stopped = false;
					} else if (check / 100 == 1) { // goal
						HitGoal ();
					} else if (check / 100 == 3) { // mine
						map.BlowMine (check % 100);
						Die ();
					} else { // asteroid
						stopped = true;
					}
				} else { // outside of bounds of map
					//stopped = true;
					Die();
				}
			} else { //outside of bounds of map
				//stopped = true;
				Die();
			}

		}

	}

	void HitGoal(){
		stopped = true;
		hitAsteroid = true;
		GameObject tmp = GameObject.FindGameObjectWithTag ("goal");
		Goal g = tmp.GetComponent<Goal> ();
		g.Win (1);

		sr.enabled = false;
		mr.enabled = false;
		scoreIcon.enabled = true;
		finished = true;
	}

	public void StartNewLevel(){
		finished = false;
		sr.enabled = true;
		mr.enabled = true;
		scoreIcon.enabled = false;
	}

}