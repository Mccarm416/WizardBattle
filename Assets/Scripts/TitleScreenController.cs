using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class that's used by the title screen to handle sound, controlling the main menu, scene loading, and fading to black upon launching the game.
 */

public class TitleScreenController : MonoBehaviour {
	//Title screen controller for handling buttons, music, and FX

	private AudioSource playBtnAudSrc;
	private AudioSource musicPlayer;

	public Button playBtn;
	public Image panelFade;
	private GameObject panelMainMenu;
	private GameObject panelCredits;
	private GameObject panelOptions;

	public Button btnLaunch;
	public Button btnOptions;
	public Button btnCredits;
	public Button btnFakeLaunch;

    private TitleScreenControls titleScreenControls;
    private List<Button> menuButtons = new List<Button>();


    void Start () {
		playBtnAudSrc = playBtn.GetComponent <AudioSource> ();
        menuButtons.Add(btnLaunch);
        menuButtons.Add(btnOptions);
        menuButtons.Add(btnCredits);
        titleScreenControls = GetComponent<TitleScreenControls>();
        panelMainMenu = GameObject.Find ("panelMainMenu");
		panelOptions = GameObject.Find ("panelOptions");
		panelCredits = GameObject.Find ("panelCredits");
		panelMainMenu.SetActive (false);
		panelOptions.SetActive (false);
		panelCredits.SetActive (false);
		panelFade.enabled = false;
	}

    private void Update()
    {
        titleScreenControls.getInput();
    }

    public void btnStartClick() {
		Debug.Log ("Start clicked");
		playBtnAudSrc.enabled = true;
		playBtnAudSrc.Play ();
		StartCoroutine (delayOpen ());
	}

	public void btnLaunchClick() {
		panelFade.enabled = true;
		StartCoroutine (fadeToBlack ());
	}

	public void btnOptionsClick() {
		panelMainMenu.SetActive (false);
		panelOptions.SetActive (true);;
	}

	public void btnOptionsBackClick() {
		panelMainMenu.SetActive (true);
		panelOptions.SetActive (false);
	}

	public void btnCreditsClick() {
		panelMainMenu.SetActive (false);
		panelCredits.SetActive (true);;
	}

	public void btnCreditsBackClick() {
		panelMainMenu.SetActive (true);
		panelCredits.SetActive (false);
	}
		
		

	IEnumerator fadeToBlack () {
		//Darkens the screen.
		panelFade.CrossFadeAlpha (20f, 2.25f, false);
		Image mainMenu = panelMainMenu.GetComponent<Image> ();
		//Fade out the Main Menu panel and buttons !THIS ISN'T WORKING FOR THE BUTTON THAT'S CLICKED!
		mainMenu.CrossFadeAlpha (0f, 2f, false);
		panelMainMenu.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		btnFakeLaunch.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnFakeLaunch.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		btnOptions.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnOptions.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		btnCredits.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnCredits.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		yield return new WaitForSeconds (3.5f);
		SceneManager.LoadScene (1);
	}

	IEnumerator delayOpen() {
		yield return new WaitForSeconds (0.5f);
		panelMainMenu.SetActive (true);
	}
}
