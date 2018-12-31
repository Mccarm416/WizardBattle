using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource menuAudio;
    public Image cursorLeft;
    public Image cursorRight;
    public Button btnResume;
    public Button btnRestart;
    public Button btnExit;
    private MenuControls menuControls;
    private Button selectedButton;
    private List<Button> menuButtons = new List<Button>();
    private int cursorPosition = 0;
    private double menuBuffer = 0;
    private BackgroundController backgroundController;


    private void Start()
    {
        menuButtons.Add(btnResume);
        menuButtons.Add(btnRestart);
        menuButtons.Add(btnExit);
        menuControls = gameObject.GetComponent<MenuControls>();
        backgroundController = GetComponent<BackgroundController>();
    }

    void Update()
    {
        if (menuControls.getStartButton())
        {
            PauseMenu();
        }
        else if(pauseMenu.activeInHierarchy)
        {
            moveCursor();
            getEnterCancel();
        }
    }
    private void getEnterCancel()
    {
        if (menuControls.getEnterButton())
        {
            selectedButton.onClick.Invoke();
        }
        else if(menuControls.getCancelButton())
        {
            PauseMenu();
        }
    }
    public void PauseMenu()
    {
        Debug.Log("PauseMenu()");
        //Calls the pause menu. A buffer of 0.5s is added between menu calls to prevent accidental opening and closing.
        if (backgroundController.gameStart)
        {
            if (!pauseMenu.activeInHierarchy)
            {
                Debug.Log("Opening pause menu");
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                menuAudio.enabled = true;
                menuAudio.Play();
                selectedButton = menuButtons[cursorPosition];
                moveCursorImages();
            }
            else 
            {
                Debug.Log("Closing pause menu");
                Time.timeScale = 1f;
                menuAudio.enabled = true;
                menuAudio.Play();
                pauseMenu.SetActive(false);
                menuBuffer = Time.time + 0.5f;
                cursorPosition = 0;
            }
            Debug.Log("PauseMenu() finished");
        }
    
    }
    private void moveCursor()
    {
        int moveCurs = menuControls.getMoveInput();
        //Check to see if there's input and the user is on the main menu
        if (moveCurs != 0)
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
            selectedButton = menuButtons[cursorPosition];
            moveCursorImages();
        }

    }
    private void moveCursorImages()
    {
        RectTransform btnTransform = selectedButton.GetComponent<RectTransform>();
        float newY = btnTransform.position.y;
        cursorRight.transform.position = new Vector3(cursorRight.transform.position.x, newY, cursorRight.transform.position.z);
        cursorLeft.transform.position = new Vector3(cursorLeft.transform.position.x, newY, cursorLeft.transform.position.z);
    }
    public void btnResumeClick()
    {
        PauseMenu();
        menuBuffer = Time.time + 0.5f;
    }

    public void btnRestartClick()
    {
        //Restart the game
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void btnExitClick()
    {
        //Exit the game
        Application.Quit();
    }

}
