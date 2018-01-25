using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTutor : MonoBehaviour {

	bool drawTutorial = true;

	bool drawIntro = true;
	bool drawMove = false;

	GameObject goalTracker;

	public GameObject blackPlayer;

	bool hasFallen = false;

	void Start(){
		goalTracker = GameObject.FindGameObjectWithTag ("GoalTracker");
	}

	void Update(){
		if (goalTracker.GetComponent<goalCount> ().youWon)
			drawTutorial = false;

		if (blackPlayer.transform.position.x < 8)
			hasFallen = true;
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
				if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Tutorial 3:\n\nThis time corgi has to jump!\nJump over the block to get its bone!\n\n\n\n(Tap here to continue)\n", customButton)) {
					InvokeRepeating ("DrawMovement", 0, 1);
					blackPlayer.GetComponent<BlackMovement> ().canMoveLeft = true;
					blackPlayer.GetComponent<BlackMovement> ().canMoveRight = true;
					drawIntro = false;
				}
			}

			if (drawMove) {
				customButton.fontSize = Screen.height / 20;

				if (hasFallen) {
					GUI.Label (new Rect (Screen.width - (Screen.width / 4), Screen.height / 4, Screen.width / 4, Screen.height - (Screen.height / 4)), "Press\n\n>", customButton);
					GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height - (Screen.height / 4)), "Press\n\nJump", customButton);
				}
				else 
					GUI.Label (new Rect (0, Screen.height / 4, Screen.width / 4, Screen.height - (Screen.height / 4)), "Press\n\n<", customButton);
			}
		}
	}
}
