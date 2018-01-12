using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class responsible for calculating the damage being dealt to a player
 */

public class DamageCalculator : MonoBehaviour {

	public int calculateDamage(Collision2D spell) {
		//Calculates the type of spell, then gives the damage to the caller
		int damage = 0;
		if (spell.gameObject.GetComponent<MissileController> () != null) {
			damage = missileHit ();
		} 
		else {
			Debug.Log ("No spell detected!!!");
		}
		return damage;
	}

	public int missileHit() {
		//Basic attack
		int baseDamage = 20;
		int totalDamage = 0;
		totalDamage = baseDamage;
		return totalDamage;
	}
		
}
