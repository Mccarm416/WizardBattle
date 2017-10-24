using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundController : MonoBehaviour {

	public Text lblHealthP1;
	public Text lblHealthP2;
	public int healthP1;
	public int healthP2;
	Player1Controller player1;
	Player2Controller player2;

	// Use this for initialization
	void Start () {
		//Get the player controllers
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent <Player2Controller> ();
		initialiseUI ();
	}

	public void initialiseUI() {
		//Update the UI
		int healthP1 = player1.Health;
		int healthP2 = player2.Health;

		lblHealthP1.text = healthP1.ToString();
		lblHealthP2.text = healthP2.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		initialiseUI ();
	}
}
