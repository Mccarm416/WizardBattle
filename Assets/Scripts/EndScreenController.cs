using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour {

	public static Player1Controller player1;
	public static Player2AIController player2;

	public Text lblP1Hits;
	public Text lblP1HitsTaken;
	public Text lblP1Health;
	public Text lblP2Hits;
	public Text lblP2HitsTaken;
	public Text lblP2Health;

	private AudioSource aSource;
	public AudioClip victoryFanfare;


	void Start () {
		aSource = GetComponent<AudioSource> ();
		aSource.enabled = true;
		aSource.clip = victoryFanfare;
		aSource.Play ();
		lblP1Hits.text = "Hits : " + player2.hitsTaken;
		lblP1HitsTaken.text = "Hits Taken : " + player1.hitsTaken;
		lblP1Health.text = "Remaining Health: " + player1.Health;

		lblP2Hits.text = "Hits : " + player2.hitsTaken;
		lblP2HitsTaken.text = "Hits Taken : " + player2.hitsTaken;
		lblP2Health.text = "Remaining Health: " + player2.Health;
	}
}
