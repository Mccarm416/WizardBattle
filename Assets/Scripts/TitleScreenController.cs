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
	public AudioClip playBtnSnd0;

	void Start () {
		playBtnAudSrc = playBtn.GetComponent <AudioSource> ();
	}

	public void playBtnClick() {
		playBtnAudSrc.enabled = true;
		playBtnAudSrc.Play ();
		SceneManager.LoadScene (1);
	}
}
