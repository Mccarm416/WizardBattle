﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour {
	//Used by first player

	private float rightX = 685f;
	private float leftX = -685f;
	private float topY = 372f;
	private float botY = -372f;
	public GameObject spell;
	DamageCalculator damageCalc;
	private Rigidbody2D rBody;
	private Transform _transform;
	private Vector2 _currentPos;
	private float fireRate = 0.5f;
	private float nextShot = 0.0f;
	private float moveHorizontal;
	private float moveVertical;
	private Vector2 newPos;
	private AudioSource playerAudio;
	private Camera camera;
	private Animator animator;
	private Player2AIController player2;

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
		animator = GetComponent<Animator> ();
		Speed = 200f;
		camera = Camera.main;
		dying = false;
		rBody = GetComponent<Rigidbody2D> ();
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		Health = 5;
		hits = 0;
		hitsTaken = 0;
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<Player2AIController> ();
	}

	void Update () {
		
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		Move ();
		if (dying) {
			//Check to see if the player is dying and if the camera should follow them
			camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		}
		else {
			//Shooting
			if (Input.GetKey (KeyCode.Space)) {
				Shoot ();
			}
		}
	}

	void Move() {
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

		else {
			//The object is a spell
			int damage = damageCalc.calculateDamage (other);
			Health -= damage;
			hitsTaken++;
			if (Health <= 0){
				Death();
			}
		}
	}

	public void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player2")) {
			rBody.isKinematic = false;
			Move ();
		}
		else if (other.gameObject.tag.Equals("border")) {
			Debug.Log("P1 -/> Border");
			rBody.isKinematic = false;
			Move ();
		}
	}

	public void Shoot() {
		if (Time.time > nextShot) {
			//Get position
			_transform = gameObject.GetComponent<Transform> ();
			_currentPos = _transform.position;
			//Get mouse location
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);	
			//Determine the direction than normalize (vector becomes a magnitude of 1)
			Vector2 direction = mousePos - _currentPos;
			direction.Normalize();
			//Create the projectile and fire it
			GameObject projectile = (GameObject)Instantiate (spell, _currentPos, Quaternion.identity);
			projectile.GetComponent<Rigidbody2D>().velocity = direction * 500;
			//Cooldown
			nextShot = Time.time + fireRate;
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
		dying = true;
		Debug.Log ("Death time: " + Time.time);
		AudioSource cameraAudio = camera.GetComponent<AudioSource> ();
		Debug.Log ("MoveCamera()");
		//Move the camera
		camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		Debug.Log ("Camera moved");
		//Changes the cameras resolution to zoom in on the dying player
		camera.orthographicSize = Mathf.Lerp (520, 60, 20);
		Time.timeScale = 1f;
		cameraAudio.clip = deathSound;
		cameraAudio.enabled = true;
		cameraAudio.Play ();
	}

	IEnumerator Scream() {
		//Decide the death scream to use
		Debug.Log ("Deciding death scream");
		int randomScream = Random.Range (1, 3);

		playerAudio = GetComponent<AudioSource> ();
		if (randomScream == 1) {
			playerAudio.clip = death01;
		} 
		else if (randomScream == 2) {
			playerAudio.clip = death02;
		}
		animator.SetTrigger ("playerDeath");
		Debug.Log ("Death scream: " + randomScream);
		playerAudio.enabled = true;
		//Wait 1 second then play the death scream
		yield return new WaitForSeconds (1.9f);
		playerAudio.Play ();
		Debug.Log ("1st return " + Time.time);
		//Wait 5 seconds then load the end screen
		yield return new WaitForSeconds (5f);
		Debug.Log ("End Screen time: " + Time.time);
		SceneManager.LoadScene(2);
	}


	void StopSound() {
		AudioSource[] audioSrcs = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];

		foreach (AudioSource aSrc in audioSrcs) {
			aSrc.Stop();
		}
	}
}
