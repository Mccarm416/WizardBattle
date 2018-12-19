using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour {
	//Will be used by second human player. Currently unused in this build.
	Rigidbody2D rBody;
	public float Speed { get; set; }
	public int Health  { get; set; }

	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		Health = 100;
		Speed = 200f;
	}

	void Update () {
		
	}
		
	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals("player1")) {
			Debug.Log("P2 -> P1");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}
		else if (other.gameObject.tag.Equals("border")) {
			Debug.Log("P2 -> P1");
			rBody.isKinematic = true;
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = 0f;
		}
	}
}
