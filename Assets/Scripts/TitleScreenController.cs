using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {
	//Title screen controller for handling buttons, music, and FX

	private AudioSource playBtnAudSrc;
	private AudioSource musicPlayer;

	public Button playBtn;
	public AudioClip music01;
	public AudioClip music02;
	public AudioClip music03;
	public AudioClip music04;
	public AudioClip playBtnSnd01;
	public AudioClip playBtnSnd02;
	public AudioClip playBtnSnd03;

	void Start () {
		initialiseMusic ();
	}

	public void initialiseMusic() {
		musicPlayer = playBtn.GetComponent<AudioSource> ();
		playBtnAudSrc = GetComponent <AudioSource> ();

		int randomMusic = Random.Range (1, 5);
		int randomPlay = Random.Range (1, 4);
		if (randomMusic == 1) {
			musicPlayer.clip = music01;
		}
		else if (randomMusic == 2) {
			musicPlayer.clip = music02;
		}
		else if (randomMusic == 3) {
			musicPlayer.clip = music03;
		}
		else if (randomMusic == 4) {
			musicPlayer.clip = music04;
		}

		if (randomMusic == 1) {
				playBtnAudSrc.clip = playBtnSnd01;
		}
		else if (randomMusic == 2) {
				playBtnAudSrc.clip = playBtnSnd02;
		}
		else if (randomMusic == 3) {
				playBtnAudSrc.clip = playBtnSnd03;
		}
		musicPlayer.enabled = true;
		musicPlayer.Play ();
	}
	public void playBtnClick() {
		playBtnAudSrc.enabled = true;
		playBtnAudSrc.Play ();
		SceneManager.LoadScene (1);
	}
}
