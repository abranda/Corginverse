using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalCount : MonoBehaviour {

	public int level;

	public bool youWon = false;

	int goals;
	int completedGoals;

	public int par;
	public int spawned;

	GameObject[] blackGoals;
	GameObject[] whiteGoals;
	int blackGoalsCount;
	int whiteGoalsCount;

	void Start(){
		spawned = GameObject.FindGameObjectsWithTag("BlackPlayer").Length + GameObject.FindGameObjectsWithTag("WhitePlayer").Length;

		blackGoals = GameObject.FindGameObjectsWithTag ("BlackGoal");
		whiteGoals = GameObject.FindGameObjectsWithTag ("WhiteGoal");

		blackGoalsCount = blackGoals.Length;
		whiteGoalsCount = whiteGoals.Length;

		goals = blackGoalsCount + whiteGoalsCount;
	}

	void Update(){
		completedGoals = 0;

		for (int i = 0; i < blackGoalsCount; i++) {
			completedGoals += blackGoals [i].GetComponent<blackGoalScore> ().scored;
		}

		for (int i = 0; i < whiteGoalsCount; i++) {
			completedGoals += whiteGoals [i].GetComponent<whiteGoalScore> ().scored;
		}

		if (completedGoals == goals) {
			youWon = true;
			if (PlayerPrefs.GetInt (level + " Player Spawned") == 0 || spawned < PlayerPrefs.GetInt (level + " Player Spawned"))
				PlayerPrefs.SetInt (level + " Player Spawned", spawned);
		}
	}

	void OnGUI(){
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = Screen.height / 10;

		if (youWon) {

			if (spawned <= par) {
				customButton.normal.textColor = new Color (0, 0.5f, 0);
				GUI.Label (new Rect (0, Screen.height / 4, Screen.width, Screen.height - (Screen.height / 4)), "You Won!\nLevel: " + level + "\n\nPar: " +  spawned + "/" + par + "\n", customButton);
			} else {
				customButton.normal.textColor = new Color (1, 0, 0);
				GUI.Label (new Rect (0, Screen.height / 4, Screen.width, Screen.height - (Screen.height / 4)), "Level Complete.\nLevel: " + level + "\n\nPar: " +  spawned + "/" + par + "\n", customButton);
			}

			customButton.fontSize = Screen.height / 20;

			if (GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Next Level", customButton)) {
				int nextScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex + 1;
				UnityEngine.SceneManagement.SceneManager.LoadScene (nextScene);
			}
			if (GUI.Button (new Rect (0, 0, Screen.width / 4, Screen.height / 4), "Restart", customButton)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
			}
			if (GUI.Button (new Rect ((Screen.width / 4) + (Screen.width / 2), 0, Screen.width / 4, Screen.height / 4), "Level Select", customButton)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (1);
			}
		}
	}
}