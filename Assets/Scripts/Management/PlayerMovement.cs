using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	GameObject[] blackPlayers;
	GameObject[] whitePlayers;

	void FixedUpdate(){

		blackPlayers = GameObject.FindGameObjectsWithTag ("BlackPlayer");
		whitePlayers = GameObject.FindGameObjectsWithTag ("WhitePlayer");

		if (Input.touches.Length > 0) {

			for (int i = 0; i < Input.touches.Length; i++) {
				if (Input.GetTouch (i).position.y < Screen.height - (Screen.height / 4)) {
					if (!(Input.GetTouch (i).position.x > Screen.width - (Screen.width / 4)) && !(Input.GetTouch (i).position.x < Screen.width / 4)  && (Input.GetTouch(i).position.y > Screen.height / 6)) {
						for (int j = 0; j < blackPlayers.Length; j++) {
							blackPlayers [j].GetComponent<BlackMovement> ().pupJump ();
						}

						for (int j = 0; j < whitePlayers.Length; j++) {
							whitePlayers [j].GetComponent<WhiteMovement> ().pupJump ();
						}
					} else if (Input.GetTouch (i).position.x > Screen.width - (Screen.width / 4)) {
						for (int j = 0; j < blackPlayers.Length; j++) {
							blackPlayers [j].GetComponent<BlackMovement> ().pupRight ();
						}

						for (int j = 0; j < whitePlayers.Length; j++) {
							whitePlayers [j].GetComponent<WhiteMovement> ().pupRight ();
						}
					} else if (Input.GetTouch (i).position.x < Screen.width / 4) {
						for (int j = 0; j < blackPlayers.Length; j++) {
							blackPlayers [j].GetComponent<BlackMovement> ().pupLeft ();
						}

						for (int j = 0; j < whitePlayers.Length; j++) {
							whitePlayers [j].GetComponent<WhiteMovement> ().pupLeft ();
						}
					}
				}
			}
		} else {
			for (int j = 0; j < blackPlayers.Length; j++) {
				blackPlayers [j].GetComponent<BlackMovement> ().left = false;
				blackPlayers [j].GetComponent<BlackMovement> ().right = false;
			}
			for (int j = 0; j < whitePlayers.Length; j++) {
				whitePlayers [j].GetComponent<WhiteMovement> ().left = false;
				whitePlayers [j].GetComponent<WhiteMovement> ().right = false;
			}
		}
	}
}
