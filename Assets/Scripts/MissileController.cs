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
		//Assign physics layer
		gameObject.layer = 10;

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
	
	// Update is called once per frame
	void Update () {
	}
}
