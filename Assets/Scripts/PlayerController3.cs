using UnityEngine;
using System.Collections;

public class PlayerController3 : MonoBehaviour {

	public static PlayerController3 S;

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
		if (inMenu && playerReady == false) {
			if(Input.GetButtonDown("A_P3") && SpawnControl.S.spawnA == false){
				spawnPoint = GameObject.Find("Spawn_A");
				SpawnControl.S.spawnA = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("B_P3") && SpawnControl.S.spawnB == false){
				spawnPoint = GameObject.Find("Spawn_B");
				SpawnControl.S.spawnB = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("X_P3") && SpawnControl.S.spawnX == false){
				spawnPoint = GameObject.Find("Spawn_X");
				SpawnControl.S.spawnX = true;
				transform.position = spawnPoint.transform.position;
				spawnPoint.SetActive (false);
				playerReady = true;
			}
			if(Input.GetButtonDown("Y_P3") && SpawnControl.S.spawnY == false){
				spawnPoint = GameObject.Find("Spawn_Y");
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
			if (Input.GetButton("A_P3") && hitAsteroid == false) {
				stopped = false;
			}

		}

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
			if(Input.GetButtonDown("X_P3")){
				myParticle.Play ();
				this.transform.position = spawnPoint.transform.position;
				stopped = true;
			}
		}


	}

	void GetInput() //gets input for playerDirection
	{

		float horzInput = Input.GetAxis ("Horizontal3");
		float vertInput = Input.GetAxis("Vertical3");


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

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "asteroid")
		{
			if(Physics.Raycast (transform.position + up, forward, length, layerMask)){
				stopped = true;
				hitAsteroid = true;
			}else{
				hitAsteroid = false;
				stopped = false;
			}
		}

		if (col.transform.tag == "wall")
		{
			CameraShake.S.shakeDuration = .5f;
			myParticle.Play();
			this.transform.position = spawnPoint.transform.position;
			stopped = true;
		}
	}

	void OnTriggerExit(Collider col){
		hitAsteroid = false;
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "asteroid") {

			if(Physics.Raycast (transform.position + up, forward, length, layerMask)){
				stopped = true;
				hitAsteroid = true;
			}else{
				hitAsteroid = false;
			}
		}
		if (col.gameObject.tag == "goal")
		{
			stopped = true;
			hitAsteroid = true;

		}
	}


}
