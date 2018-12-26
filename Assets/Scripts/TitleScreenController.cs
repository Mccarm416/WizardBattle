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

	public Image panelFade;
	private GameObject panelMainMenu;
	private GameObject panelCredits;
	private GameObject panelOptions;

    public Image cursorLeft;
    public Image cursorRight;

    public Text txtVersion;
    public Button btnStart;
    public Button btnLaunch;
    public Button btnFakeLaunch;
    public Button btnOptions;
    public Button btnOptionsBack;
	public Button btnCredits;
    public Button btnCreditsBack;

    private Button selectedButton;

    private bool gameStart = false;
    private int cursorPosition = 0;
    private TitleScreenControls titleScreenControls;
    private List<Button> menuButtons = new List<Button>();


    void Start () {
        selectedButton = btnStart;
		playBtnAudSrc = btnStart.GetComponent <AudioSource> ();
        menuButtons.Add(btnLaunch);
        menuButtons.Add(btnOptions);
        menuButtons.Add(btnCredits);
        titleScreenControls = GetComponent<TitleScreenControls>();
        panelMainMenu = GameObject.Find ("panelMainMenu");
		panelOptions = GameObject.Find ("panelOptions");
		panelCredits = GameObject.Find ("panelCredits");
        cursorLeft.enabled = false;
        cursorRight.enabled = false;
		panelMainMenu.SetActive (false);
		panelOptions.SetActive (false);
		panelCredits.SetActive (false);
		panelFade.enabled = false;
	}
    private void Update()
    {
        if (!gameStart)
        {
            getStart();
            moveCursor();
            getEnterCancel();
        }
        
    }

    private void getEnterCancel()
    {
        if(titleScreenControls.getEnterButton())
        {
            selectedButton.onClick.Invoke();
        }
        else if (titleScreenControls.getCancelButton())
        {
            if(selectedButton == btnOptionsBack || selectedButton == btnCreditsBack)
            {
                selectedButton.onClick.Invoke();
            }
        }
    }
    private void getStart()
    {
        if(!panelMainMenu.activeInHierarchy && titleScreenControls.getStartButton())
        {
            selectedButton.onClick.Invoke();
        }
    }
    private void moveCursor()
    {
        int moveCurs = titleScreenControls.getMoveInput();
        //Check to see if there's input and the user is on the main menu
        if (moveCurs != 0 && panelMainMenu.activeInHierarchy)
        {
            cursorPosition = cursorPosition + moveCurs;
            //Check to see if the player is at the bottom of the list and is going down. 
            if (cursorPosition > menuButtons.Count - 1)
            {
                //Move cursor to the top of the list
                cursorPosition = 0;

            }
            //Check to see if the player is at the top of the list and is going up
            else if (cursorPosition < 0)
            {
                //Move cursor to the bottom of the list
                cursorPosition = menuButtons.Count - 1;
            }
            moveCursorImages(menuButtons[cursorPosition]);
        }

    }

    private void moveCursorImages(Button newButton)
    {
        selectedButton = newButton;
        RectTransform btnTransform = newButton.GetComponent<RectTransform>();
        float newY = btnTransform.position.y;
        cursorRight.transform.position = new Vector3(cursorRight.transform.position.x, newY, cursorRight.transform.position.z);
        cursorLeft.transform.position = new Vector3(cursorLeft.transform.position.x, newY, cursorLeft.transform.position.z);
    }
    public void btnStartClick() {
		playBtnAudSrc.enabled = true;
		playBtnAudSrc.Play ();
		StartCoroutine (delayOpen ());
        selectedButton = btnLaunch;
	}

	public void btnLaunchClick() {
        gameStart = true;
		panelFade.enabled = true;
        playBtnAudSrc.Play();
		StartCoroutine (fadeToBlack ());
	}

	public void btnOptionsClick() {
		panelMainMenu.SetActive (false);
		panelOptions.SetActive (true);
        moveCursorImages(btnOptionsBack);
	}

	public void btnOptionsBackClick() {
		panelMainMenu.SetActive (true);
		panelOptions.SetActive (false);
        moveCursorImages(menuButtons[cursorPosition]);
	}

	public void btnCreditsClick() {
		panelMainMenu.SetActive (false);
		panelCredits.SetActive (true);
        moveCursorImages(btnCreditsBack);
	}

	public void btnCreditsBackClick() {
		panelMainMenu.SetActive (true);
		panelCredits.SetActive (false);
        moveCursorImages(menuButtons[cursorPosition]);
    }



    IEnumerator fadeToBlack () {
		//Darkens the screen.
		panelFade.CrossFadeAlpha (20f, 2.25f, false);
		Image mainMenu = panelMainMenu.GetComponent<Image> ();
		//Fade out the Main Menu panel and buttons
		mainMenu.CrossFadeAlpha (0f, 2f, false);
        cursorRight.CrossFadeAlpha(0f, 2f, false);
        cursorLeft.CrossFadeAlpha(0f, 2f, false);
        panelMainMenu.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);//This doesn't grab txtVersion for some reason so it must be done manually below
        txtVersion.CrossFadeAlpha(0f, 2f, false);
        btnFakeLaunch.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnFakeLaunch.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		btnOptions.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnOptions.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
		btnCredits.GetComponent<Image>().CrossFadeAlpha (0f, 2f, false);
		btnCredits.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 2f, false);
        cursorLeft.CrossFadeAlpha(0f, 1.5f, false);
        cursorRight.CrossFadeAlpha(0f, 1.5f, false);


        yield return new WaitForSeconds (3.5f);
		SceneManager.LoadScene (1);
	}

	IEnumerator delayOpen() {
		yield return new WaitForSeconds (0.5f);
		panelMainMenu.SetActive (true);
        cursorLeft.enabled = true;
        cursorRight.enabled = true;
    }
}
