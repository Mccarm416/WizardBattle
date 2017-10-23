
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {


	private AudioSource _explosionSound;
	private AudioSource _pickupSound;
	bool blinkActive;
	[SerializeField]
	GameObject explosion;
	[SerializeField]
	GameController gameController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other) {
		//Detects collision with objects and responds accordingly
		//Checks to see if blink is active to prevent player from being hit by enemies
		if (!blinkActive) {
			if (other.gameObject.tag.Equals ("enemy01")) {
				Debug.Log ("Collision enemy01\n");
				//Play sound
				_explosionSound = other.gameObject.GetComponent<AudioSource> ();
				if (_explosionSound != null)
					_explosionSound.Play ();

				//Create explosion
				Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
				//Lose life
				gameController.Life--;
				//Reset enemy position
				other.gameObject.GetComponent<EnemyController01> ().Reset ();
				//Blink the player
				StartCoroutine ("Blink");

			} else if (other.gameObject.tag.Equals ("enemy02")) {
				Debug.Log ("Collision enemy02\n");
				//Play sound
				_explosionSound = other.gameObject.GetComponent<AudioSource> ();
				if (_explosionSound != null)
					_explosionSound.Play ();
				//PLay explosion animation
				Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
				//Lose life
				gameController.Life--;
				other.gameObject.GetComponent<EnemyController02> ().Reset ();
				//Blink the player
				StartCoroutine ("Blink");
			} else if (other.gameObject.tag.Equals ("enemy03")) {
				Debug.Log ("Collision enemy03\n");
				//Play sound
				_explosionSound = other.gameObject.GetComponent<AudioSource> ();
				if (_explosionSound != null)
					_explosionSound.Play ();
				//Play explosion animation
				Instantiate (explosion).GetComponent<Transform> ().position = other.gameObject.GetComponent<Transform> ().position;
				//Lose life
				gameController.Life--;
				//Reset enemy position
				other.gameObject.GetComponent<EnemyController03> ().Reset ();
				//Blink the player
				StartCoroutine ("Blink");
			}
		}
		if (other.gameObject.tag.Equals ("pickup")) {
			Debug.Log ("Collision pickup\n");
			//Set the sound and play it
			_pickupSound = other.gameObject.GetComponent<AudioSource> ();
			if (_pickupSound != null)
				_pickupSound.Play ();
			//Gain points
			gameController.Score += 100;
			//Reset pickup position
			other.gameObject.GetComponent<PickupController> ().Reset ();
		}
	}


	private IEnumerator Blink(){
		//Flashes player transparency when hitting enemy
		Color c;
		blinkActive = true;
		Renderer renderer = gameObject.GetComponent<Renderer> ();
		for (int i = 0; i < 2; i++) {
			//Changes the alpha of the object
			for (float f = 1f; f >= 0; f -= 0.5f) {
				c = renderer.material.color;
				c.a = f + 0.2f;
				renderer.material.color = c;
				yield return new WaitForSeconds (.5f);
			}
			for (float f = 0f; f <= 1; f += 0.5f) {
				c = renderer.material.color;
				c.a = f + 0.2f;
				renderer.material.color = c;
				yield return new WaitForSeconds (.5f);
			}
		}
		blinkActive = false;
	}
}
*/
