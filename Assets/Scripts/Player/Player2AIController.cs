using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class responsible for controlling player 2 character. Holds their player values as well as methods for AI, movement/boundaries, animations, shooting/targeting, death event, and collisions.
 */

public class Player2AIController : Player {
	//AI script

	Transform _transform;
	Vector2 _currentPos;
	Rigidbody2D rBody;
	public float Speed { get; set; }
	private Camera camera;
	private AudioSource playerAudio;

	//AI values
	private float idealDistance; //AIs ideal target distance between them and the player
	private int deadArea; //Dead area the AI is comfortable in
	private int lowHealth; //AI becomes defensive when his health becomes this low
	private int offensiveCheck; //AI becomes more aggresive when this number is greater then their HP against the players(Not yet implemented)
	private float distance; //Distance from AI to player
	private Vector2 enemyPos; //Co-ordinates of the enemy player
	private Player1Controller player1;
	private Vector2 travelPos; //AIs next positon
	private Animator animator;

	private bool isEnabled { get; set; }
    private float prevSpeed;

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
		idealDistance = 200;
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
			//Move ();
			//Shoot ();
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
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
			animator.SetBool ("playerMove", true);
		} 
		else if ((distance - idealDistance) < idealDistance - deadArea) {
			//Move away
			travelPos = new Vector2 (enemyPos.x + _currentPos.x, enemyPos.y + _currentPos.y);
			travelPos.Normalize ();
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
			animator.SetBool ("playerMove", true);
		}
		else {
			//Player is in the deadzone
			animator.SetBool ("playerMove", false);
		}
	}

	public void Shoot() {
        double basicAttackNS = GetComponent<CastBasicAttack>().nextShot;
        double fireLionNS = GetComponent<CastFireLion>().nextShot;
        if (fireLionNS < Time.time && distance <= 200)
        {
            Debug.Log("P2AIC - Casting Fire Lion");

            gameObject.GetComponent<CastFireLion>().castFireLion();
        }
        else if (basicAttackNS < Time.time)
        {
            gameObject.GetComponent<CastBasicAttack>().castBasicAttack();

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
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}
	}

	public void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player1")) {
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
		Destroy(GetComponent<PolygonCollider2D>());
		Player1Controller player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		Player2Controller player2 = GetComponent<Player2Controller> ();
		EndScreenController.player1 = player1;
		EndScreenController.player2 = player2;
		//Stop the players from moving and shooting
		player2.Speed = 0;
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

	public override void disable() {
		if (enabled) {
			prevSpeed = Speed;
			Speed = 0;
			enabled = false;
			Debug.Log ("Player 2 disabled");
		}
		else {
			Debug.Log ("Player 2 is already disabled");
		}
	}
	public override void enable() {
		if (!enabled) {
			Debug.Log ("Player 2 enabled");
			Speed = prevSpeed;
			enabled = true;
		}
		else {
			Debug.Log ("Player 2 is already enabled");
		}
	}

    void onTakeDamage(int damage)
    {
        Debug.Log("P2 - onTakeDamage");
        Health = Health - damage;
        hitsTaken++;
        if (Health <= 0)
        {
            Death();
        }
    }
}
