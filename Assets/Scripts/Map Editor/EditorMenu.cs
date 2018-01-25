using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMenu : MonoBehaviour {

	public Texture2D orangeButton;
	public Texture2D purpleButton;

	bool delete, edit, upload = false;

	GameObject GameManager;

	void Start(){
		GameManager = GameObject.FindGameObjectWithTag ("GameManager");
		GameManager.GetComponent<CustomManager> ().tileMap = null;
	}

	void OnGUI() {
		GUIStyle customLabel = new GUIStyle ("label");
		customLabel.fontSize = Screen.height / 7;
		GUI.contentColor = Color.white;
		GUI.Label (new Rect (Screen.width / 15, Screen.height / 30, Screen.width, Screen.height), "Custom Levels", customLabel);

		float width = Screen.width / 16;
		float height = Screen.height / 4;
		float xOffset = ((Screen.width / (100 / 93.75f)) - (Screen.width / 16)) / 4;
		float yOffset = width * (5 / 2);

		GUIStyle customButton = new GUIStyle ("button");
		customButton.normal.textColor = Color.black;
		customButton.fontSize = Screen.height / 20;

		for (int i = 1; i < 5; i++) {

			string buttonText = "";

			if (delete) {
				buttonText = "Delete Level " + i.ToString ();
			} else if (edit) {
				buttonText = "Edit Level " + i.ToString ();
			} else if (upload) {
				buttonText = "Upload Level " + i.ToString ();
			} else if(GameManager.GetComponent<CustomManager>().fileExists(i)) {
				if (GameManager.GetComponent<CustomManager> ().hasSetPar (i))
					buttonText = "Play Level " + i.ToString ();
				else
					buttonText = "Edit Level " + i.ToString ();
			} else
				buttonText = "Empty";

			if(i % 2 == 0)
				customButton.normal.background = orangeButton;
			else 
				customButton.normal.background = purpleButton;
			
			if (buttonText.Equals("Edit Level " + i.ToString()) || (buttonText.Equals ("Empty") && !delete && !upload)) {
				if (GUI.Button (new Rect (width + ((i - 1) * xOffset), height, xOffset, yOffset), buttonText, customButton)) {
					edit = true;
					LevelChoice (i);
				}
			} else {
				if (GUI.Button (new Rect (width + ((i - 1) * xOffset), height, xOffset, yOffset), buttonText, customButton)) {
					LevelChoice (i);
				}
			}
		}


		//Custom Menu Items
		if (delete) {
			customButton.normal.background = purpleButton;
			if (GUI.Button (new Rect (width + (3 * xOffset), Screen.height / 30, xOffset, yOffset - (Screen.height / 30)), "Cancel Delete", customButton)) {
				delete = false;
			}
		} else {
			customButton.normal.background = purpleButton;
			if (GUI.Button (new Rect (width + (3 * xOffset), Screen.height / 30, xOffset, yOffset - (Screen.height / 30)), "Delete", customButton)) {
				delete = true;
				edit = false;
				upload = false;
			}
		}
		if (edit) {
			customButton.normal.background = orangeButton;
			if (GUI.Button (new Rect (width, height + yOffset + (Screen.height / 25), 2 * xOffset, yOffset), "Cancel Edit", customButton)) {
				edit = false;
			}
		} else {
			customButton.normal.background = orangeButton;
			if (GUI.Button (new Rect (width, height + yOffset + (Screen.height / 25), 2 * xOffset, yOffset), "Edit", customButton)) {
				edit = true;
				upload = false;
				delete = false;
			}
		}
		customButton.normal.background = purpleButton;
		if (GUI.Button (new Rect (width, height + (2 * yOffset) + (Screen.height / 25), 2 * xOffset, yOffset), "Level Select", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
		}
		if (GUI.Button (new Rect (width + (2 * xOffset), height + yOffset + (Screen.height / 25), 2 * xOffset, yOffset), "Download\n(Future Update)", customButton)) {

		}
		if (upload) {
			customButton.normal.background = orangeButton;
			if (GUI.Button (new Rect (width + (2 * xOffset), height + (2 * yOffset) + (Screen.height / 25), 2 * xOffset, yOffset), "Cancel Upload\n(Future Update)", customButton)) {
				upload = false;
			}
		} else {
			customButton.normal.background = orangeButton;
			if (GUI.Button (new Rect (width + (2 * xOffset), height + (2 * yOffset) + (Screen.height / 25), 2 * xOffset, yOffset), "Upload\n(Future Update)", customButton)) {
				upload = true;
				edit = false;
				delete = false;
			}
		}
	}

	void LevelChoice(int level){
		if (delete) {
			GameManager.GetComponent<CustomManager> ().level = level;
			if (GameManager.GetComponent<CustomManager> ().fileExists ())
				System.IO.File.Delete (GameManager.GetComponent<CustomManager> ().file);
			delete = false;
		} else if (edit) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (4);
			GameManager.GetComponent<CustomManager> ().level = level;
		} else if (upload && GameManager.GetComponent<CustomManager>().fileExists(level)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (5);
			GameManager.GetComponent<CustomManager> ().level = level;
		} else {
			if (GameManager.GetComponent<CustomManager> ().fileExists (level)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (3);
				GameManager.GetComponent<CustomManager> ().level = level;
			}
		}
		delete = false;
		edit = false;
		upload = false;
	}
}
