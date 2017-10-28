using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	private Camera camera;

	public AudioClip death01;
	public  AudioClip death02;

	public int Health { get; set; }
	public int hits { get; set; }
	public int hitsTaken { get; set; }

	bool dying;
	public AudioClip deathSound;

	void Start () {
		camera = Camera.main;
		dying = false;
		rBody = GetComponent<Rigidbody2D> ();
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		Health = 5;
		hits = 0;
		hitsTaken = 0;
	}

	void Update () {
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		Move ();
		//Shooting
		if (Input.GetKey (KeyCode.Space)) {
			Shoot ();
		}
		if (dying) {
			camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		}
	}

	void Move() {
		//Movement
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		CheckBoundary ();
		newPos.x = moveHorizontal * speed * Time.deltaTime;
		newPos.y = moveVertical * speed * Time.deltaTime;

		transform.Translate (newPos);

			//transform.Translate (0f, moveVertical * speed * Time.deltaTime, 0f);
	}

	private void CheckBoundary() {
		//Checks the players position against the camera boundary to prevent them from moving off of it
		if (_currentPos.x + (moveHorizontal* speed * Time.deltaTime) < leftX) {
			Debug.Log ("curPos < leftX");
			moveHorizontal = 0;
		}
		if (_currentPos.x + (moveHorizontal* speed * Time.deltaTime) > rightX) {
			Debug.Log ("curPos > rightX");
			moveHorizontal = 0;
		}
		if (_currentPos.y + (moveVertical* speed * Time.deltaTime) > topY) {
			Debug.Log ("curPos > topY");
			moveVertical = 0;
		}
			if (_currentPos.y + (moveVertical * speed * Time.deltaTime) < botY) {
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
		//Objects that will be used during the end screen
		Player2Controller player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<Player2Controller> ();
		//EndScreenController.endScreenScript.player1 = GetComponent<Player1Controller> ();
		//EndScreenController.endScreenScript.player2 = player2;

		//Stops the other player from shooting
		//Destroy (player2.gameObject);
		//Stop all sound then play the death sound
		StopSound();
		StartCoroutine(MoveCamera());
		StartCoroutine (Scream ());
	}

	IEnumerator MoveCamera() {
		dying = true;
		AudioSource cameraAudio = camera.GetComponent<AudioSource> ();
		Debug.Log ("MoveCamera()");
		camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		Debug.Log ("Camera moved");
		//Changes the cameras resolution
		camera.orthographicSize = Mathf.Lerp (520, 60, 20);
		Time.timeScale = 0.1f;
		cameraAudio.clip = deathSound;
		cameraAudio.enabled = true;
		cameraAudio.Play ();
		yield return new WaitForSeconds (1f);
	}

	IEnumerator Scream() {
		Time.timeScale = 0.1f;
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
		Debug.Log ("Death scream: " + randomScream);
		playerAudio.enabled = true;
		playerAudio.Play ();
		yield return new WaitForSeconds (0.6f);
		Time.timeScale = 1f;
		SceneManager.LoadScene(2);
	}

	void StopSound() {
		AudioSource[] audioSrcs = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];

		foreach (AudioSource aSrc in audioSrcs) {
			aSrc.Stop();
		}
	}
}
