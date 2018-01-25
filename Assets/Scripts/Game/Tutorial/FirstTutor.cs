using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutor : MonoBehaviour {

	bool drawTutorial = true;

	bool drawIntro = true;
	bool drawMove = false;

	GameObject goalTracker;

	public GameObject blackPlayer;

	void Start(){
		goalTracker = GameObject.FindGameObjectWithTag ("GoalTracker");
	}

	void Update(){
		blackPlayer.GetComponent<BlackMovement> ().canJump = false;
		if (goalTracker.GetComponent<goalCount> ().youWon)
			drawTutorial = false;
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
				if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Tutorial 1:\n\nWelcome Corgi Master!\nMove your corgi to its bone!\n\n\n\n(Tap here to continue)\n", customButton)) {
					InvokeRepeating ("DrawMovement", 0, 1);
					blackPlayer.GetComponent<BlackMovement> ().canMoveRight = true;
					drawIntro = false;
				}
			}

			if (drawMove) {
				customButton.fontSize = Screen.height / 20;

				GUI.Label (new Rect (Screen.width - (Screen.width / 4), Screen.height / 4, Screen.width / 4, Screen.height - (Screen.height / 4)), "Press\n\n>", customButton);
			}
		}
	}
}