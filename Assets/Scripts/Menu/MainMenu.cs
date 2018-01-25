using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GameObject GameManagerPrefab;
	GameObject GameManager;
	bool clearScore = false;

	public Texture2D orangeButton;

	void Start(){

		if (GameManager == null){
			if (GameObject.FindGameObjectWithTag ("GameManager") == null)
				Instantiate (GameManagerPrefab);
			GameManager = GameObject.FindGameObjectWithTag ("GameManager");
		}
	}

	void OnGUI(){
		GUIStyle customLabel = new GUIStyle ("label");
		customLabel.normal.textColor = Color.black;
		customLabel.fontSize = Screen.height / 7;

		GUI.Label (new Rect (Screen.width / 20, (Screen.height / 7), Screen.width, Screen.height), "Corginverse", customLabel);

		GUIStyle customButton = new GUIStyle ("button");
		customButton.normal.textColor = Color.black;
		customButton.fontSize = Screen.height / 15;
		customButton.normal.background = orangeButton;

		if (GUI.Button (new Rect(Screen.width - ((Screen.height / 7) * 5), Screen.height / 7, (Screen.height / 7) * 4, (Screen.height / 7) * 2), "Level Select", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("LevelSelect");
		}
		if (GUI.Button (new Rect(Screen.width - ((Screen.height / 7) * 5), (Screen.height / 7) * 3.5f, (Screen.height / 7) * 4, (Screen.height / 7) * 2), "Next Level", customButton)) {
			nextLevel ();
		}

		customButton.fontSize = Screen.height / 30;

		if (GUI.Button (new Rect(Screen.width - ((Screen.height / 7) * 5), Screen.height - (Screen.height / 6), (Screen.height / 7) * 4, Screen.height / 8), "Credits", customButton)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (6);
		}

		if (!clearScore) {
			if (GUI.Button (new Rect ((Screen.height / 7), Screen.height - (Screen.height / 6), (Screen.height / 7) * 4, Screen.height / 8), "Clear Saved Data", customButton)) {
				clearScore = true;
			}
		} else if (clearScore) {
			if (GUI.Button (new Rect ((Screen.height / 7), Screen.height - (Screen.height / 6), (Screen.height / 7) * 4, Screen.height / 8), "Are You Sure?", customButton)) {
				clearScore = false;
				PlayerPrefs.DeleteAll ();
			}
		}
	}

	void nextLevel(){
		for (int i = 1; i < 41; i++) {
			if (PlayerPrefs.GetInt (i + " Player Spawned") == 0 || PlayerPrefs.GetInt (i + " Player Spawned") > GameManager.GetComponent<LevelPar>().getPar(i-1)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (i + 6);
				break;
			}
		}
	}
}