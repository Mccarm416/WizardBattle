using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

	/*
	private Transform _transform;
	private Vector2 _currentPos;
	private GameObject missile;
*/
	void Start () {
		Player1Controller player1;
		Player2AIController player2;
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent <Player2AIController> ();
		float distanceP1 = Vector2.Distance (transform.position, player1.transform.position);
		float distanceP2 = Vector2.Distance (transform.position, player2.transform.position);
		if (distanceP2 > distanceP1) {
			gameObject.layer = 10;
		}
		else {
			gameObject.layer = 11;
		}
		//Trying to seperate the spell into a seperate class
		//Cooldown should be handled by player class
		/*
		missile = GameObject.Find ("p1Missile");

		//Get position
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		//Get mouse location
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);	
		//Determine the direction than normalize (vector becomes a magnitude of 1)
		Vector2 direction = mousePos - _currentPos;
		direction.Normalize();
		//Create the projectile and fire it
		GameObject projectile = (GameObject)Instantiate (gameObject, _currentPos, Quaternion.identity);
		projectile.GetComponent<Rigidbody2D>().velocity = direction * 500;
		*/
	}

	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.GetComponent <MissileController>() != null) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
