using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

	//Public Variables
	public float speed = 200;
	[SerializeField]
	private float rightX = 683f;
	[SerializeField]
	private float leftX = -683f;
	[SerializeField]
	private float topY = 365f;
	[SerializeField]
	private float botY = -365f;
	[SerializeField]
	private float fireRate = 0.5f;
	[SerializeField]
	GameObject spell;
	public DamageCalculator damageCalc;
	//Private variables
	Rigidbody2D rBody;
	private Transform _transform;
	private Vector2 _currentPos;
	private float nextShot = 0.0f;
	private bool canWalk; //Used for collision detection to stop movement
	private float moveHorizontal;
	private float moveVertical;

	public int Health { get; set; }

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		Health = 100;
	}
	
	// Update is called once per frame
	void Update () {

		//Movement
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		transform.Translate (moveHorizontal * speed * Time.deltaTime, 0f, 0f);
		transform.Translate (0f, moveVertical * speed * Time.deltaTime, 0f);

		//Shooting
		if (Input.GetKey (KeyCode.Space)) {
			Shoot ();

		}
		CheckBoundary ();
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

	private void CheckBoundary() {
		//Checks the players position against the camera boundary to prevent them from moving off of it
		if (_currentPos.x < leftX) {
			_currentPos.x = leftX;
		}
		if (_currentPos.x > rightX) {
			_currentPos.x = rightX;
		}
		if (_currentPos.y > topY) {
			_currentPos.y = topY;
		}
		if (_currentPos.y < botY) {
			_currentPos.y = botY;
		}
	}


	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player2")) {
			Debug.Log("P1 -> P2");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}

		else {
			//The object is a spell
			int damage = damageCalc.calculateDamage (other);
			Health -= damage;
		}
	}

	public void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player2")) {
			Debug.Log("P1 -/> P2");
			rBody.isKinematic = false;
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
			transform.Translate (moveHorizontal * speed * Time.deltaTime, 0f, 0f);
			transform.Translate (0f, moveVertical * speed * Time.deltaTime, 0f);
		}
	}
}
