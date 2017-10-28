using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundController : MonoBehaviour {
	//Gameplays UI and music controller


	public Text lblHealthP1;
	public Text lblHealthP2;
	public int healthP1;
	public int healthP2;
	Player1Controller player1;
	Player2AIController player2;


	//For audio
	public AudioClip music01;
	public AudioClip music02;
	private AudioSource musicPlayer;

	private float menuBuffer;
	GameObject pauseMenu;
	public Button btnRestart;
	public Button btnResume;
	AudioSource audioSrc;

	void Start () {
		//Get the player controller scripts
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent <Player2AIController> ();
		pauseMenu = GameObject.Find ("pauseMenuPanel");
		pauseMenu.SetActive (false);
		audioSrc = pauseMenu.GetComponent<AudioSource>();
		menuBuffer = 0f;
		initialiseMusic ();
		initialiseUI ();
	}

	void Update () {
		initialiseUI ();
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Escape hit");
			PauseMenu ();
		}
	}

	public void initialiseUI() {
		//Update the UI
		int healthP1 = player1.Health;
		int healthP2 = player2.Health;

		lblHealthP1.text = healthP1.ToString();
		lblHealthP2.text = healthP2.ToString();
	}

	public void initialiseMusic() {
		int randomMusic = Random.Range (1, 3);
		musicPlayer = GetComponent<AudioSource> ();
		if (randomMusic == 1) {
			musicPlayer.clip = music01;
		}
		else if (randomMusic == 2) {
			musicPlayer.clip = music02;
		}
		musicPlayer.Play ();
	}

	public void btnResumeClick() {
		//Resume gameplay
		Debug.Log("Resume clicked");
		Time.timeScale = 1f;
		audioSrc.enabled = true;
		audioSrc.Play ();
		pauseMenu.SetActive (false);
		menuBuffer = Time.time + 0.5f;
	}

	public void btnRestartClick() {
		//Restart the game
		SceneManager.LoadScene(1);
	}

	public void PauseMenu() {
		//Calls the pause menu. A buffer of 0.5s between menu calls is added to prevent accidental opening and closing.
		if (Time.timeScale == 1f && Time.time > menuBuffer) {
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			audioSrc.enabled = true;
			audioSrc.Play ();
			} 
			else if (Time.timeScale == 0f) {
				Time.timeScale = 1f;
				audioSrc.enabled = true;
				audioSrc.Play ();
				pauseMenu.SetActive (false);
				menuBuffer = Time.time + 0.5f;
			}
	}
}
