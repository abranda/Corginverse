using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GoalCountEditor : MonoBehaviour {

	GameObject GameManager;

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

	public bool play = false;

	void Start() {
		GameManager = GameObject.FindGameObjectWithTag("GameManager");

		level = GameManager.GetComponent<CustomManager> ().level;

		spawned = GameObject.FindGameObjectsWithTag("BlackPlayer").Length + GameObject.FindGameObjectsWithTag("WhitePlayer").Length;

		blackGoals = GameObject.FindGameObjectsWithTag ("BlackGoal");
		whiteGoals = GameObject.FindGameObjectsWithTag ("WhiteGoal");

		blackGoalsCount = blackGoals.Length;
		whiteGoalsCount = whiteGoals.Length;

		goals = blackGoalsCount + whiteGoalsCount;

		if (play) {
			string file = GameManager.GetComponent<CustomManager> ().file;
			StreamReader mapFile = new StreamReader (file);

			for (int i = 0; i < 5; i++) {
				mapFile.ReadLine ();
			}
			par = int.Parse(mapFile.ReadLine ());
		}
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
		}
	}

	void OnGUI(){
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = Screen.height / 10;



		if (youWon) {

			if (!play) {
				customButton.fontSize = Screen.height / 10;

				GUI.Label (new Rect (0, Screen.height / 4, Screen.width, Screen.height - (Screen.height / 4)), "Custom Level: " + level + "\n\nPar Set: " + spawned, customButton);

				customButton.fontSize = Screen.height / 20;
				if (GUI.Button (new Rect (0, 0, Screen.width / 2, Screen.height / 4), "Back to Editor", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (4);
				}
				if (GUI.Button (new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height / 4), "Save and Exit", customButton)) {
					GameManager.GetComponent<CustomManager> ().par = spawned;
					GameObject.FindGameObjectWithTag ("EditorObject").GetComponent<CustomEditor> ().hasEdited = false;
					GameObject.FindGameObjectWithTag ("EditorObject").GetComponent<CustomEditor> ().Save ();
					UnityEngine.SceneManagement.SceneManager.LoadScene (2);
				}
			} else {
				customButton.fontSize = Screen.height / 10;

				if (spawned <= par) {
					customButton.normal.textColor = new Color (0, 0.5f, 0);
					GUI.Label (new Rect (0, Screen.height / 4, Screen.width, Screen.height - (Screen.height / 4)), "You Won!\nLevel: " + level + "\n\nPar: " +  spawned + "/" + par + "\n", customButton);
				} else {
					customButton.normal.textColor = new Color (1, 0, 0);
					GUI.Label (new Rect (0, Screen.height / 4, Screen.width, Screen.height - (Screen.height / 4)), "Level Complete.\nLevel: " + level + "\n\nPar: " +  spawned + "/" + par + "\n", customButton);
				}
				customButton.fontSize = Screen.height / 20;

				if (GUI.Button (new Rect (0, 0, Screen.width / 4, Screen.height / 4), "Restart", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (3);
				}
				if (GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Editor Menu", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (2);
				}
				if (GUI.Button (new Rect ((Screen.width / 4) + (Screen.width / 2), 0, Screen.width / 4, Screen.height / 4), "Main Menu", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (0);
				}
			}
		}
	}
}
