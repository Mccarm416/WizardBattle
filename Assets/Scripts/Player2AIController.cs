using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class responsible for controlling player 2 character. Holds their player values as well as methods for AI, movement/boundaries, animations, shooting/targeting, death event, and collisions.
 */

public class Player2AIController : MonoBehaviour {
	//AI script

	DamageCalculator damageCalc;
	Transform _transform;
	Vector2 _currentPos;
	Rigidbody2D rBody;
	public int Health  { get; set; }
	public float Speed { get; set; }
	public int hits { get; set; }
	public int hitsTaken { get; set; }
	public float fireRate {get; set;}
	private Camera camera;
	private AudioSource playerAudio;

	//AI values
	private float idealDistance; //AIs ideal target distance between them and the player
	private int deadArea; //Dead area the AI is comfortable in
	private int lowHealth; //AI becomes defensive when his health becomes this low
	private int offensiveCheck; //AI becomes more aggresive when this number is greater then their HP against the players(Not yet implemented)
	private float distance; //Distance from AI to player
	private Vector2 enemyPos; //Co-ordinates of the enemy player
	private float nextShot; //Countdown until next shot.
	private int missileSpeed; //Speed at which the missile moves
	private Player1Controller player1;
	private Vector2 travelPos; //AIs next positon
	private float rightX = 685f;
	private float leftX = -685f;
	private float topY = 372f;
	private float botY = -372f;
	private Animator animator;

	private bool isEnabled { get; set; }
	private float prevFR;
	private float prevSpeed;

	public GameObject spell;

	//Death variables
	bool dying;
	public AudioClip deathSound;
	public AudioClip death01;
	public  AudioClip death02;

	void Start () {
		enabled = true;
		Speed = 180f;
		fireRate = 1f;
		missileSpeed = 500;
		Health = 1;
		animator = GetComponent<Animator> ();
		camera = Camera.main;
		dying = false;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		rBody = GetComponent<Rigidbody2D> ();
		idealDistance = 200;
		nextShot = 0.0f;
		deadArea = 10;
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		rBody.freezeRotation = true;
	}

	void Update () {
		if (!dying && enabled) {
			//Re-initialise variables if the character is not dead
			distance = Vector2.Distance(player1.transform.position, transform.position);
			_transform = GetComponent<Transform> ();
			_currentPos = _transform.position;
			enemyPos = player1.transform.position;
			Move ();
			Shoot ();
		}
		else if (dying){
			camera.transform.position = new Vector3 (transform.position.x, transform.position.y + 10, -10);
		}
	}

	public void Move() {
		//Check distance
		//Look at creating an alternating idealDistance for more dynamic play

		//Determine to move away or not. The greater/less than checks creates a dead deadspace.
		if ((distance - idealDistance) > idealDistance + deadArea) {
			//Move closer
			travelPos = new Vector2 (enemyPos.x - _currentPos.x, enemyPos.y - _currentPos.y);
			travelPos.Normalize ();
			CheckBoundary ();
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
			animator.SetBool ("playerMove", true);
		} 
		else if ((distance - idealDistance) < idealDistance - deadArea) {
			//Move away
			travelPos = new Vector2 (enemyPos.x + _currentPos.x, enemyPos.y + _currentPos.y);
			travelPos.Normalize ();
			CheckBoundary ();
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
			animator.SetBool ("playerMove", true);
		}
		else {
			//Player is in the deadzone
			animator.SetBool ("playerMove", false);
		}
	}

	private void CheckBoundary() {
		//Checks the players position against the camera boundary to prevent them from moving off of it
		if (_currentPos.x + (travelPos.x * Speed * Time.deltaTime) < leftX) {
			Debug.Log ("curPos < leftX");
			travelPos.x = 0;
		}
		if (_currentPos.x + (travelPos.x * Speed * Time.deltaTime) > rightX) {
			Debug.Log ("curPos > rightX");
			travelPos.x = 0;
		}
		if (_currentPos.y + (travelPos.y * Speed * Time.deltaTime) > topY) {
			Debug.Log ("curPos > topY");
			travelPos.y = 0;
		}
		if (_currentPos.y + (travelPos.y * Speed * Time.deltaTime) < botY) {
			Debug.Log ("curPos < botY");
			travelPos.y = 0;
		}
	}

	public void Shoot() {
		if (Time.time > nextShot) {
			Debug.Log ("P2 Shooting");
			//Get position
			_transform = gameObject.GetComponent<Transform> ();
			_currentPos = _transform.position;
			//Get player location
			Vector2 mousePos = player1.transform.position;	
			//Determine the direction than normalize (vector becomes a magnitude of 1)
			Vector2 direction = mousePos - _currentPos;
			direction.Normalize();
			//Create the projectile and fire it
			GameObject projectile = (GameObject)Instantiate (spell, _currentPos, Quaternion.identity);
			//Calculate the angle for missile rotation
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			//Rotate the missile
			projectile.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			projectile.GetComponent<Rigidbody2D>().velocity = direction * missileSpeed;
			//Cooldown
			nextShot = Time.time + fireRate;
		}
	}

	//Collision methods
	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player1")) {
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}
		else if (other.gameObject.tag.Equals("border")) {
			Debug.Log("P2 -> Border");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}

		else {
			//The object is a spell
			int damage = damageCalc.calculateDamage (other);
			Health -= damage;
			hitsTaken++;
			if (Health <= 0) {
				Death ();
			}
		}
	}

	public void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player1")) {
			Debug.Log("P2 -/> P1");
			rBody.isKinematic = false;
			Move ();
		}
		else if (other.gameObject.tag.Equals("border")) {
			Debug.Log("P1 -/> Border");
			rBody.isKinematic = false;
			Move ();
		}
	}

	public void Death() {
		//Pass player controllers to the next scenes controller
		Destroy(GetComponent<PolygonCollider2D>());
		Player1Controller player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		Player2AIController player2 = GetComponent<Player2AIController> ();
		EndScreenController.player1 = player1;
		EndScreenController.player2 = player2;
		//Stop the players from moving and shooting
		player2.Speed = 0f;
		player2.fireRate = 0f;
		Destroy (player1.gameObject);
		//Stop all sound then start the cinematic death
		StopSound();
		MoveCamera();
		StartCoroutine (Scream ());
	}

	void MoveCamera() {
		//Method used to move the camera and zoom it up to the player when they die
		dying = true;
		Debug.Log ("Death time: " + Time.time);
		AudioSource cameraAudio = camera.GetComponent<AudioSource> ();
		Debug.Log ("MoveCamera()");
		camera.transform.position = new Vector3 (transform.position.x, transform.position.y+10, -10);
		Debug.Log ("Camera moved");
		//Increases the cameras resolution to zoom in on the dying player
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
		Debug.Log ("Death scream: " + randomScream);
		playerAudio.enabled = true;
		animator.SetTrigger ("playerDeath");
		//Wait 1 second then play the death scream
		yield return new WaitForSeconds (1.5f);
		playerAudio.Play ();
		Debug.Log ("1st return " + Time.time);
		//Wait 5 seconds then load the end screen
		yield return new WaitForSeconds (5f);
		Debug.Log ("End Screen time: " + Time.time);
		UnityEngine.SceneManagement.SceneManager.LoadScene(2);
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
			Debug.Log ("Player 2 disabled");
		}
		else {
			Debug.Log ("Player 2 is already disabled");
		}
	}
	public void enable() {
		if (!enabled) {
			Debug.Log ("Player 2 enabled");
			Speed = prevSpeed;
			fireRate = prevFR;
			enabled = true;
		}
		else {
			Debug.Log ("Player 2 is already enabled");
		}
	}
}
