using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

	string creditText =
		"Brought to you by :\nAnthonyBrandaCS.com\n\n" +
		"Programming/Game Play :\nAnthony Branda\n\n" +
		"Art :\nRobert Branda\nand\nChristopher Bennett\n";// + 
		//"Music :\nMaybe in a future update?";

	public Texture2D orangeButton;

	void OnGUI(){

		GUIStyle customLabel = new GUIStyle ("label");
		customLabel.normal.textColor = Color.black;
		customLabel.fontSize = Screen.height / 7;

		GUI.Label (new Rect (Screen.width / 20, Screen.height / 7, Screen.width, Screen.height), "Corginverse", customLabel);

		customLabel.fontSize = Screen.height / 22;
		GUI.Label (new Rect (Screen.width - ((Screen.height / 7) * 5), Screen.height / 10, (Screen.height / 7) * 4, Screen.height - (Screen.height / 7)), creditText, customLabel);

		GUIStyle customButton = new GUIStyle ("button");
		customButton.normal.textColor = Color.black;
		customButton.fontSize = Screen.height / 30;
		customButton.normal.background = orangeButton;

		if (GUI.Button (new Rect ((Screen.height / 7), Screen.height - (Screen.height / 6), (Screen.height / 7) * 4, Screen.height / 8), "Main Menu", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (0);
		}

		if (GUI.Button (new Rect(Screen.width - ((Screen.height / 7) * 5), Screen.height - (Screen.height / 6), (Screen.height / 7) * 4, Screen.height / 8), "Visit: AnthonyBrandaCS.com\n(Under Development)", customButton)) {
			Application.OpenURL ("http://AnthonyBrandaCS.com/");
		}
	}
}
