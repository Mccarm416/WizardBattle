using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class responsible for controlling player 1 character. Holds their player values as well as methods for movement/boundaries, animations, shooting, death event, and collisions.
 */

public class Player1Controller : Player { 
	//Used by first player

	private float rightX = 685f;
	private float leftX = -685f;
	private float topY = 372f;
	private float botY = -372f;
	private Rigidbody2D rBody;
	private Transform _transform;
	private Vector2 _currentPos;

    DamageCalculator damageCalc;
    private float moveHorizontal;
	private float moveVertical;
	private Vector2 newPos;
	private AudioSource playerAudio;
	private Camera camera;
	private Animator animator;
	private Player2AIController player2;



	private bool isEnabled { get; set; }
	private float prevFR;
	private float prevSpeed;

	public float fireRate { get; set; }
	public int Health { get; set; }
	public int hits { get; set; }
	public int hitsTaken { get; set; }
	public float Speed { get; set; }


	//Death variables
	bool dying;
	public AudioClip deathSound;
	public AudioClip death01;
	public  AudioClip death02;

	void Start () {
		enabled = true;
		Speed = 200f;
		Health = 100;
		animator = GetComponent<Animator> ();
		camera = Camera.main;
		dying = false;
		rBody = GetComponent<Rigidbody2D> ();
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		hits = 0;
		hitsTaken = 0;
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<Player2AIController> ();

	}

	void Update () {
		
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;

		if (!dying && enabled) {
			//Move and shoot
			Move ();
			if (Input.GetButton("Fire1") || Input.GetAxis("Right Trigger") > 0) {
				//Shoot ();
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
               // fireLion();
			}
		} 
		else if (dying) {
			//Check to see if the player is dying and if the camera should follow them
			camera.transform.position = new Vector3 (transform.position.x, transform.position.y + 10, -10);
		}
	}


	protected override void Move() {
		//Controls player movement
		//Get player input
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		//Check to see if player is running up against a screen boundary
		CheckBoundary ();

		//Calculating the new point to move to
		newPos.x = moveHorizontal * Speed * Time.deltaTime;
		newPos.y = moveVertical * Speed * Time.deltaTime;
		//Check to see if movement animation should play (this should be snappier)
		if (moveHorizontal != 0 || moveVertical != 0) {
			animator.SetBool ("playerMove", true);
		}
		else {
			animator.SetBool ("playerMove", false);
		}
		//Move to the new position
		transform.Translate (newPos);
	}

	private void CheckBoundary() {
		//Checks the players position against the camera boundary to prevent them from moving off of it
		if (_currentPos.x + (moveHorizontal* Speed * Time.deltaTime) < leftX) {
			Debug.Log ("curPos < leftX");
			moveHorizontal = 0;
		}
		if (_currentPos.x + (moveHorizontal* Speed * Time.deltaTime) > rightX) {
			Debug.Log ("curPos > rightX");
			moveHorizontal = 0;
		}
		if (_currentPos.y + (moveVertical* Speed * Time.deltaTime) > topY) {
			Debug.Log ("curPos > topY");
			moveVertical = 0;
		}
		if (_currentPos.y + (moveVertical * Speed * Time.deltaTime) < botY) {
			Debug.Log ("curPos < botY");
			moveVertical = 0;
		}
	}

	//Collision methods
	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player2")) {
			Debug.Log("P1 -> P2");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}

		else if (other.gameObject.tag.Equals("border")) {
			Debug.Log("P1 -> Border");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}
	}

	public void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player2")) {
			rBody.isKinematic = false;
			Move ();
		}
		else if (other.gameObject.tag.Equals("border")) {
			rBody.isKinematic = false;
			Move ();
		}
	}


	public void Death() {
		//Pass player controllers to the next scenes controller
		Player1Controller player1 = GetComponent<Player1Controller> ();
		Destroy (GetComponent<PolygonCollider2D> ());
		EndScreenController.player1 = player1;
		EndScreenController.player2 = player2;
		//Stop the players from moving and shooting
		player1.Speed = 0f;
		player1.fireRate = 0f;
		Destroy (player2.gameObject);
		//Stop all sound then start the cinematic death
		StopSound();
		MoveCamera();
		StartCoroutine (Scream ());
	}

	void MoveCamera() {
		//Method used to move the camera and zoom it up to the player when they die
		dying = true;
		AudioSource cameraAudio = camera.GetComponent<AudioSource> ();
		//Move the camera
		camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		//Increases the cameras resolution to zoom in on the dying player
		camera.orthographicSize = Mathf.Lerp (520, 60, 20);
		Time.timeScale = 1f;
		cameraAudio.clip = deathSound;
		cameraAudio.enabled = true;
		cameraAudio.Play ();
	}

	IEnumerator Scream() {
		//Decide the death scream to use

		//Generate a random number between 1-2 to select a death scream
		int randomScream = Random.Range (1, 3);
		playerAudio = GetComponent<AudioSource> ();
		if (randomScream == 1) {
			playerAudio.clip = death01;
		} 
		else if (randomScream == 2) {
			playerAudio.clip = death02;
		}
		//Play the death animation
		animator.SetTrigger ("playerDeath");
		playerAudio.enabled = true;
		//Wait 1 second then play the death scream
		yield return new WaitForSeconds (1.9f);
		playerAudio.Play ();
		//Wait 5 seconds then load the end screen
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene(2);
	}


	void StopSound() {
		AudioSource[] audioSrcs = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];

		foreach (AudioSource aSrc in audioSrcs) {
			aSrc.Stop();
		}
	}

	public void disable() {
		if (enabled) {
			prevSpeed = Speed;
			prevFR = fireRate;
			fireRate = 0;
			Speed = 0;
			enabled = false;
			Debug.Log ("Player 1 disbaled");
		}
		else {
			Debug.Log ("Player 1 is already disabled");
		}
	}
	public void enable() {
		if (!enabled) {
			Debug.Log ("Player 1 enabled");
			Speed = prevSpeed;
			fireRate = prevFR;
			enabled = true;
		}
		else {
			Debug.Log ("Player 1 is already enabled");
		}
	}

    void onTakeDamage(int damage)
    {
        Health = Health - damage;
        hitsTaken++;
        if (Health <= 0)
        {
            Death();
        }
    }
}
