using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthTutor : MonoBehaviour {

	bool drawTutorial = true;

	bool drawIntro = true;
	bool drawMove = false;

	GameObject goalTracker;

	public GameObject blackPlayer;
	GameObject whitePlayer;

	bool overBlack = false;
	bool spawnMessage = false;
	bool goalMessage = false;

	void Start(){
		goalTracker = GameObject.FindGameObjectWithTag ("GoalTracker");
	}

	void Update(){
		if (goalTracker.GetComponent<goalCount> ().youWon)
			drawTutorial = false;

		if (blackPlayer.transform.position.x > 7)
			overBlack = true;

		if (whitePlayer == null) {
			whitePlayer = GameObject.FindGameObjectWithTag ("WhitePlayer");
			if (whitePlayer != null) {
				whitePlayer.GetComponent<WhiteMovement> ().canMoveLeft = false;
				whitePlayer.GetComponent<WhiteMovement> ().canMoveRight = false;
				whitePlayer.GetComponent<WhiteMovement> ().canJump = false;
			}
		}
	}

	void DrawMovement(){
		drawMove = !drawMove;
	}

	void OnGUI(){

		if (drawTutorial) {
			GUIStyle customButton = new GUIStyle ("button");
			customButton.fontSize = Screen.height / 20;
			GUI.contentColor = new Color (127, 127, 127);

			if (drawIntro) {
				if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Tutorial 4:\n\nMove your corgi to the grass!\nWe will get the bone to our left later!\nThings are about to get interesting!\n\n\n(Tap here to continue)\n", customButton)) {
					InvokeRepeating ("DrawMovement", 0, 1);
					blackPlayer.GetComponent<BlackMovement> ().canMoveLeft = true;
					blackPlayer.GetComponent<BlackMovement> ().canMoveRight = true;
					blackPlayer.GetComponent<BlackMovement> ().canJump = true;
					drawIntro = false;
				}
			}

			if (overBlack && !spawnMessage && !goalMessage) {
				blackPlayer.GetComponent<BlackMovement> ().canMoveLeft = false;
				blackPlayer.GetComponent<BlackMovement> ().canMoveRight = false;
				blackPlayer.GetComponent<BlackMovement> ().canJump = false;

				if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Tutorial 4:\n\nYour corgi can split in two!\nSplit your corgi Corgi Master!\n\n\n\n(Tap here to continue)\n", customButton)) {
					spawnMessage = true;
				}
			} else if (overBlack && spawnMessage && !goalMessage && whitePlayer == null && blackPlayer.GetComponent<BlackSpawn>().canSpawn) {
				GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "\n\n\n\n\n\nPress 'Corgi Split'", customButton);
			} else if (overBlack && spawnMessage && !goalMessage && whitePlayer != null) {
				if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Tutorial 4:\n\nWow!\nWho knew corgis could do that?\nNow Corgi Master,\nmove the corgis to their bones!\n\n(Tap here to continue)\n", customButton)) {
					goalMessage = true;
					blackPlayer.GetComponent<BlackMovement> ().canMoveLeft = true;
					blackPlayer.GetComponent<BlackMovement> ().canMoveRight = true;
					blackPlayer.GetComponent<BlackMovement> ().canJump = true;

					whitePlayer.GetComponent<WhiteMovement> ().canMoveLeft = true;
					whitePlayer.GetComponent<WhiteMovement> ().canMoveRight = true;
					whitePlayer.GetComponent<WhiteMovement> ().canJump = true;
				}
			} else {
				if (drawMove && whitePlayer != null) {
					GUI.Label (new Rect (0, Screen.height / 4, Screen.width / 4, Screen.height - (Screen.height / 4)), "Press\n\n<", customButton);
					GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Press\n\nJump", customButton);
				}
			}
		}
	}
}
