using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomPlay : MonoBehaviour {

	public GameObject goalTracker;
	public GameObject[] playTile;

	string file;

	int[,] tileMap;
	GameObject[,] objectMap;

	GameObject GameManager;

	void Start(){
		GameManager = GameObject.FindGameObjectWithTag ("GameManager");

		tileMap = new int[14, 5];
		objectMap = new GameObject[14, 5];

		if (GameManager.GetComponent<CustomManager> ().fileExists ()) {

			file = GameManager.GetComponent<CustomManager> ().file;

			StreamReader mapFile = new StreamReader (file);

			for (int y = 4; y >= 0; y--) {
//				for (int x = 0; x < 14; x++) {
//					tileMap [x, y] = mapFile.Read () - 48;
//					mapFile.Read ();
//				}
//				mapFile.Read ();
				char[] split = { ' ' };
				string[] Line = mapFile.ReadLine().Split(split);
				for (int x = 0; x < 14; x++) {
					tileMap [x, y] = int.Parse(Line[x]);
				}
			}
			mapFile.Close ();
		}

		Play ();
	}

	void Play() {
		GameManager.GetComponent<CustomManager> ().tileMap = tileMap;

		GameObject goal = (GameObject)Instantiate (goalTracker);
		goal.GetComponent<GoalCountEditor> ().play = true;

		Debug.Log ("We are playing using CustomPlay!");

		for (int x = 0; x < 14; x++) {
			for (int y = 0; y < 5; y++) {
				DestroyObject (objectMap [x, y]);

				if (tileMap [x, y] == 8) {
					objectMap [x, y] = (GameObject)Instantiate (playTile [4], new Vector2 (x + 1, y + 1), Quaternion.identity);
					Instantiate (playTile [3], new Vector2 (x + 1, y + 1), Quaternion.identity);
					objectMap [x, y].GetComponent<InFocus> ().isFocus = true;
				} else if (tileMap [x, y] == 9) {
					objectMap [x, y] = (GameObject)Instantiate (playTile [5]);
					objectMap [x, y].transform.position = new Vector2 (x + 1, y + 1);
					Instantiate (playTile [2], new Vector2 (x + 1, y + 1), Quaternion.identity);
					objectMap [x, y].GetComponent<InFocus> ().isFocus = true;
				} else {
					if (tileMap [x, y] == 4) {
						objectMap [x, y] = (GameObject)Instantiate (playTile [tileMap [x, y]], new Vector2 (x + 1, y + 1), Quaternion.identity);
						Instantiate (playTile [3], new Vector2 (x + 1, y + 1), Quaternion.identity);
					} else if (tileMap [x, y] == 5) {
						objectMap [x, y] = (GameObject)Instantiate (playTile [tileMap [x, y]]);
						objectMap [x, y].transform.position = new Vector2 (x + 1, y + 1);
						Instantiate (playTile [2], new Vector2 (x + 1, y + 1), Quaternion.identity);
					} else
						objectMap [x, y] = (GameObject)Instantiate (playTile [tileMap [x, y]], new Vector2 (x + 1, y + 1), Quaternion.identity);
				}
			}
		}
	}
}
