using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

	/*
	[SerializeField]
	float speed = 11f;

	private Transform _transform;
	private Vector2 _currentPos;
	private AudioSource _explosionSound;
	[SerializeField]
	GameObject explosion;

	// Use this for initialization
	void Start () {
	_transform = gameObject.GetComponent<Transform> ();
	_currentPos = _transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Sets the speed of the bullet and destroys it if offscreen
		_currentPos = _transform.position;
		_transform.position = _currentPos;
		_currentPos += new Vector2 (speed, 0);

		if (_currentPos.x >= 415) {
			Destroy (gameObject);
		}
		_transform.position = _currentPos;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("bullet -> enemy");
		if (other.gameObject.tag.Equals("enemy01")) {
			Debug.Log (other);
			//Play sound
			_explosionSound = other.gameObject.GetComponent<AudioSource> ();
			if (_explosionSound != null)
				_explosionSound.Play ();

			//Create explosion
			Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
			//Reset enemy position
			other.gameObject.GetComponent<EnemyController01> ().Reset ();
			Destroy (gameObject);
		}
		else if (other.gameObject.tag.Equals("enemy02")) {
			Debug.Log (other);
			//Play sound
			_explosionSound = other.gameObject.GetComponent<AudioSource> ();
			if (_explosionSound != null)
				_explosionSound.Play ();

			//Create explosion
			Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
			//Reset enemy position
			other.gameObject.GetComponent<EnemyController02> ().Reset ();
			Destroy (gameObject);
		}
		else if (other.gameObject.tag.Equals("enemy03")) {
			Debug.Log (other);
			//Play sound
			_explosionSound = other.gameObject.GetComponent<AudioSource> ();
			if (_explosionSound != null)
				_explosionSound.Play ();

			//Create explosion
			Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
			//Reset enemy position
			other.gameObject.GetComponent<EnemyController03> ().Reset ();
			Destroy (gameObject);
		}

	}

*/
}
