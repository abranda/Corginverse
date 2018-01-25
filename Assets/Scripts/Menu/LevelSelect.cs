using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

	public Texture2D orangeButton;
	public Texture2D purpleButton;

	GameObject GameManager;
	int level = 1;

	void Update(){
		if (GameManager == null)
			GameManager = GameObject.FindGameObjectWithTag ("GameManager");
	}

	void OnGUI(){

		GUIStyle customLabel = new GUIStyle ("label");
		customLabel.fontSize = Screen.height / 7;
		GUI.contentColor = Color.white;
		GUI.Label (new Rect (Screen.width / 15, Screen.height / 30, Screen.width, Screen.height), "Level Select", customLabel);

		float width = Screen.width / 16;
		float height = Screen.height / 4;
		float xOffset = ((Screen.width / (100 / 93.75f)) - (Screen.width / 16)) / 4;
		float yOffset = width * (5 / 2);

		GUIStyle customButton = new GUIStyle ("button");
		customButton.normal.textColor = Color.black;
		customButton.fontSize = Screen.height / 15;
		if (level > 1) {
			customButton.normal.background = purpleButton;
			if (GUI.Button (new Rect (Screen.height / 60, height, width - Screen.height / 30, yOffset * 2), "<", customButton)) {
				level -= 8;
			}
		}
		if (level < 32) {
			customButton.normal.background = purpleButton;
			if (GUI.Button (new Rect (xOffset * 4 + width + (Screen.height / 60), height, width - Screen.height / 30, yOffset * 2), ">", customButton)) {
				level += 8;
			}
		}
		int lev = level;
		for (int y = 0; y < 2; y++) {
			for (int x = 0; x < 4; x++) {

				if (y == 0) {
					if (x % 2 == 0)
						customButton.normal.background = orangeButton;
					else
						customButton.normal.background = purpleButton;
				} else {
					if (x % 2 == 1)
						customButton.normal.background = orangeButton;
					else
						customButton.normal.background = purpleButton;
				}

				if (PlayerPrefs.GetInt (lev + " Player Spawned") == 0)
					customButton.normal.textColor = Color.black;
				else if (PlayerPrefs.GetInt (lev + " Player Spawned") > GameManager.GetComponent<LevelPar> ().getPar (lev - 1))
					customButton.normal.textColor = new Color (1, 0, 0);
				else if (PlayerPrefs.GetInt (lev + " Player Spawned") <= GameManager.GetComponent<LevelPar> ().getPar (lev - 1))
					customButton.normal.textColor = new Color (0, 0.625f, 0);

				if (GUI.Button (new Rect (width + x * xOffset, height + y * yOffset, xOffset, yOffset), "Level " + lev.ToString () + "\nPar: " + PlayerPrefs.GetInt(lev + " Player Spawned") + "/" + GameManager.GetComponent<LevelPar>().getPar(lev-1), customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (lev + 6);
				}
				lev++;
			}
		}
		customButton.normal.textColor = Color.black;
		customButton.normal.background = orangeButton;
		if (GUI.Button (new Rect (width, height + (2 * yOffset) + (Screen.height / 25), 2 * xOffset, yOffset), "Main Menu", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (0);
		}
		customButton.normal.background = purpleButton;
		if (GUI.Button (new Rect (width + (2 * xOffset), height + (2 * yOffset) + (Screen.height / 25), 2 * xOffset, yOffset), "Custom Levels", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (2);
		}
	}
}