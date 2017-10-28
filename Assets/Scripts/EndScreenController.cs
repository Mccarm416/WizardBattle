using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour {

	public static EndScreenController endScreenScript;
	public static int winningPlayer;
	public static Player1Controller player1;
	public static Player2Controller player2;

	public Text lblP1Hits;
	public Text lblP1HItsTaken;
	public Text lblP1Health;
	public Text lblP2Hits;
	public Text lblP2HItsTaken;
	public Text lblP2Health;


	void Start () {
		Debug.Log (player1.Health);
		lblP1Hits.text = "Hits : " + player1.hits;
		lblP1HItsTaken.text = "Hits Taken : " + player1.hitsTaken;
		lblP1Health.text = "Remaining Health: " + player1.Health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
