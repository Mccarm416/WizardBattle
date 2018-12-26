using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class responsible for controlling all the UI elements, initiating the games countdown, sound, and pause menu functions.
 */

public class BackgroundController : MonoBehaviour {
	//Gameplays UI and music controller


	public Text lblHealthP1;
	public Text lblHealthP2;
	Player1Controller player1;
	Player2Controller player2;

    public Slider healthP1;
    public Slider healthP2;
    public Image panelBattleUI;
    

	private float menuBuffer;
	GameObject pauseMenu;
	public Button btnRestart;
	public Button btnResume;
	public Button btnExit;
	AudioSource audioSrc;
	private bool gameStart;

	private Image panelReady;
	public Text lblReady;
	public Text lblGo;
	public AudioClip audioGo;
	public AudioClip audioReady;

	void Start () {
		//Get the player controller scripts
		player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller> ();
		player2 = GameObject.FindGameObjectWithTag("player2").GetComponent <Player2Controller> ();
		gameStart = false;
		pauseMenu = GameObject.Find ("pauseMenuPanel");
		pauseMenu.SetActive (false);
        panelBattleUI.gameObject.SetActive(false);
        lblHealthP1.enabled = false;
        lblHealthP2.enabled = false;
		audioSrc = pauseMenu.GetComponent<AudioSource>();
		panelReady = GameObject.Find ("panelReady").GetComponent<Image> ();
		menuBuffer = 0f;
		StartCoroutine (ReadyFight ());
	}

	void Update () {
        updateHealth();
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
			PauseMenu ();
		}
        
	}

    private void updateHealth()
    {
        lblHealthP1.text = player1.Health.ToString();
        lblHealthP2.text = player2.Health.ToString();
        float p1HpValue = (float)player1.Health / 100;
        float p2HpValue = (float)player2.Health / 100;
        healthP1.value = p1HpValue;
        healthP2.value = p2HpValue;
    }


	IEnumerator ReadyFight() {
		//Ready... Fight! played at start of round
		AudioSource aS = GameObject.Find ("panelReady").GetComponent<AudioSource>();
		aS.enabled = true;
		aS.clip = audioReady;
		aS.Play ();
		lblGo.enabled = false;
		player1.disable ();
		player2.disable ();
		//Darkens the panel
		panelReady.CrossFadeAlpha (2.8f, 3f, false);
		yield return new WaitForSeconds (3f);
		//Hide the panel
		panelReady.CrossFadeAlpha (0f, 0.3f, false);
		player1.enable();
		player2.enable ();
		aS.clip = audioGo;
		aS.Play ();
        panelBattleUI.gameObject.SetActive(true);
        lblReady.enabled = false;
		lblGo.enabled = true;
		lblHealthP1.enabled = true;
		lblHealthP2.enabled = true;
		gameStart = true;
		yield return new WaitForSeconds (1.4f);
		lblGo.enabled = false;
		Destroy(GameObject.Find("panelReady"));
	}

	public void btnResumeClick() {
		//Resume gameplay
		Time.timeScale = 1f;
		audioSrc.enabled = true;
		audioSrc.Play ();
		pauseMenu.SetActive (false);
		menuBuffer = Time.time + 0.5f;
	}

	public void btnRestartClick() {
		//Restart the game
		SceneManager.LoadScene(1);
		Time.timeScale = 1f;
	}

	public void btnExitClick() {
		//Exit the game
		Application.Quit ();
	}

	public void PauseMenu() {
		//Calls the pause menu. A buffer of 0.5s is added between menu calls to prevent accidental opening and closing.
		if (gameStart) {
			if (lblGo != null && lblGo.isActiveAndEnabled == false) {
				panelReady.enabled = false;
				lblGo.enabled = false;
			}
			Debug.Log ("Checking timescale...");
			if (Time.timeScale == 1f && Time.time > menuBuffer) {
				Debug.Log ("Opening pause menu");
				pauseMenu.SetActive (true);
				Time.timeScale = 0f;
				audioSrc.enabled = true;
				audioSrc.Play ();
			} else if (Time.timeScale == 0f) {
				Debug.Log ("Closing pause menu");
				Time.timeScale = 1f;
				audioSrc.enabled = true;
				audioSrc.Play ();
				pauseMenu.SetActive (false);
				menuBuffer = Time.time + 0.5f;
			}
			Debug.Log ("PauseMenu() finished");
		}	
	}
}
