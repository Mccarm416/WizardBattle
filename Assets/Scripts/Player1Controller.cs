using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {
	//Used by first player
	public float speed = 200f;
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

	public AudioClip death01;
	public  AudioClip death02;

	bool boundCheckX;
	bool boundCheckY;
	public int Health { get; set; }

	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		Health = 5;
	}

	void Update () {
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		boundCheckX = false;
		boundCheckY = false;
		Move ();
		//Shooting
		if (Input.GetKey (KeyCode.Space)) {
			Shoot ();
		}
	}

	void Move() {
		//Movement
		//Debug.Log("Horizontal :" +moveHorizontal);
		//Debug.Log("Vertical :" +moveVertical);
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		CheckBoundary ();
		//Debug.Log("Horizontal2 :" +moveHorizontal);
		//Debug.Log("Vertical2 :" +moveVertical);
		newPos.x = moveHorizontal * speed * Time.deltaTime;
		newPos.y = moveVertical * speed * Time.deltaTime;

		transform.Translate (newPos);

			//transform.Translate (0f, moveVertical * speed * Time.deltaTime, 0f);
	}

	private void CheckBoundary() {
		//Checks the players position against the camera boundary to prevent them from moving off of it
		if (_currentPos.x + moveHorizontal < leftX) {
			Debug.Log ("curPos < leftX");
			moveHorizontal = 0;
		}
		if (_currentPos.x + moveHorizontal > rightX) {
			Debug.Log ("curPos > rightX");
			moveHorizontal = 0;
		}
		if (_currentPos.y + moveVertical > topY) {
			Debug.Log ("curPos > topY");
			moveVertical = 0;
		}
		if (_currentPos.y + moveVertical < botY) {
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
			if (Health >= 0){
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
		int randomMusic = Random.Range (1, 3);
		playerAudio = GetComponent<AudioSource> ();
		if (randomMusic == 1) {
			playerAudio.clip = death01;
		}
		else if (randomMusic == 2) {
			playerAudio.clip = death02;
		}
		playerAudio.enabled = true;
		playerAudio.Play ();
	}
}
