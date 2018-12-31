using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class responsible for controlling the end screens UI and button methods.
 */

public class EndScreenController : MonoBehaviour {

	public static Player1Controller player1;
	public static Player2Controller player2;
    public Text lblP1;
    public Text lblP2;
    public Image imgCrown;
    public Image imgTrophyLeft;
    public Image imgTrophyRight;
	public Text lblWinner;
	public Text lblP1Hits;
	public Text lblP1HitsTaken;
	public Text lblP1Health;
	public Text lblP2Hits;
	public Text lblP2HitsTaken;
	public Text lblP2Health;
	public Button btnRestart;
	public Button btnExit;


	public void btnRestartClick() {
		//Restart the game
		SceneManager.LoadScene(1);
	}

	public void btnExitClick() {
		//Exit the game
		Application.Quit ();
	}

	void Start () {
		if (player1.Health > player2.Health) {
			lblWinner.text = "Player 1 Wins!";
		}
		else {
			lblWinner.text = "Player 2 Wins!";
		}
		lblP1Hits.text = "Hits : " + player1.hitsTaken;
		lblP1HitsTaken.text = "Hits Taken : " + player1.hitsTaken;
		lblP1Health.text = "Remaining Health: " + player1.Health;

		lblP2Hits.text = "Hits : " + player2.hitsTaken;
		lblP2HitsTaken.text = "Hits Taken : " + player2.hitsTaken;
		lblP2Health.text = "Remaining Health: " + player2.Health;
        placeAwards();
	}

    private void placeAwards()
    {
        if(player1.Health > player2.Health)
        {
            Vector2 lblPlayerPos = lblP1.transform.position;

            imgCrown.transform.position = new Vector2(lblPlayerPos.x, lblPlayerPos.y + (lblP1.preferredHeight / 2 + 5));
            imgTrophyRight.transform.position = new Vector2(lblPlayerPos.x + (lblP1.preferredWidth), lblPlayerPos.y);
            imgTrophyLeft.transform.position = new Vector2(lblPlayerPos.x - (lblP1.preferredWidth - 5), lblPlayerPos.y);
        }
    }
}
