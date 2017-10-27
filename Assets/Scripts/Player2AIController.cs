using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2AIController : MonoBehaviour {
	//AI script

	DamageCalculator damageCalc;
	Transform _transform;
	Vector2 _currentPos;
	Rigidbody2D rBody;
	public int Health  { get; set; }
	public float Speed { get; set; }
	private float idealDistance; //AIs ideal target distance between them and the player
	private int deadArea; //Dead area the AI is comfortable in
	private int lowHealth; //AI becomes defensive when his health becomes this low
	private int offensiveCheck; //AI becomes more aggresive when this number is greater then their HP against the players
	private float distance; //Distance from AI to player
	private Vector2 enemyPos; //Co-ordinates of the enemy player
	private float nextShot; //Countdown until next shot
	private float fireRate;//Rate of fire control
	private Player1Controller player1;

	public GameObject spell;

	void Start () {
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		rBody = GetComponent<Rigidbody2D> ();
		Health = 100;	
		Speed = 200f;
		idealDistance = 200;
		fireRate = 0.5f;
		nextShot = 0.0f;
		deadArea = 10;
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		rBody.freezeRotation = true;
	}

	void Update () {
		//Re-initialise variables
		distance = Vector2.Distance(player1.transform.position, transform.position);
		_transform = GetComponent<Transform> ();
		_currentPos = _transform.position;
		enemyPos = player1.transform.position;

		Move ();
		Shoot ();
	}
		
	public void Move() {
		//Check distance
		//Look at creating an alternating idealDistance for more dynamic play
		Vector2 travelPos;
		//Determine to move away or not. The greater/less than checks creates a dead deadspace.
		if ((distance - idealDistance) > idealDistance + deadArea) {
			//Move closer
			travelPos = new Vector2 (enemyPos.x - _currentPos.x, enemyPos.y - _currentPos.y);
			travelPos.Normalize ();
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
		} 
		else if ((distance - idealDistance) < idealDistance - deadArea) {
			//Move away
			travelPos = new Vector2 (enemyPos.x + _currentPos.x, enemyPos.y + _currentPos.y);
			travelPos.Normalize ();
			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
		}
		else {
			/*
			 * Working on a circling mechanism
			 * 
			_currentPos.Normalize ();
			travelPos = new Vector2 (_currentPos.x + _currentPos.x, _currentPos.y + _currentPos.y);

			_transform.Translate (travelPos.x * Speed * Time.deltaTime, 0f, 0f);
			_transform.Translate (0f, travelPos.y * Speed * Time.deltaTime, 0f);
			*/
		}

	}
	public void Shoot() {
		if (Time.time > nextShot) {
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
			projectile.GetComponent<Rigidbody2D>().velocity = direction * 500;
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
}
