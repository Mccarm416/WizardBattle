using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour {

	DamageCalculator damageCalc;
	public int Health  { get; set; }

	void Start () {
		damageCalc = gameObject.GetComponent<DamageCalculator>();
		Health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals ("player1")) {
		} 
		else {
			//The object is a spell
			int damage = damageCalc.calculateDamage (other);
			Health -= damage;
		}
	}
}
