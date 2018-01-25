using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomEditor : MonoBehaviour {

	public GameObject levelCamera;

	bool playing = false;
	bool drawPlayTiles = false;

	GameObject GameManager;
	string file;

	int[,] tileMap;
	GameObject[,] objectMap;

	public int currentTile = 1;
	public GameObject[] editorTile;
	public GameObject[] playTile;

	public Texture2D[] buttonTextures;
	GUIStyle[] buttonTile;

	public GameObject goalTracker;
	bool focus = false;

	public bool hasEdited = false;

	void Start(){

		buttonTile = new GUIStyle[8];

		for (int i = 0; i < buttonTextures.Length; i++) {
			buttonTile [i] = new GUIStyle ();
			buttonTile [i].normal.background = buttonTextures [i];
		}

		GameManager = GameObject.FindGameObjectWithTag("GameManager");

		tileMap = new int[14, 5];
		objectMap = new GameObject[14, 5];

		if (GameManager.GetComponent<CustomManager> ().tileMap == null) {
			if (GameManager.GetComponent<CustomManager> ().fileExists ()) {
			
				file = GameManager.GetComponent<CustomManager> ().file;

				StreamReader mapFile = new StreamReader (file);

				for (int y = 4; y >= 0; y--) {
					char[] split = { ' ' };
					string[] Line = mapFile.ReadLine().Split(split);
					for (int x = 0; x < 14; x++) {
						tileMap [x, y] = int.Parse(Line[x]);
						if (tileMap [x, y] == 8 || tileMap [x, y] == 9)
							focus = true;
						
						objectMap [x, y] = (GameObject)Instantiate (editorTile [tileMap [x, y]], new Vector2 (x + 1, y + 1), Quaternion.identity);
					}
				}
				mapFile.Close ();
			} else {
				for (int y = 4; y >= 0; y--) {
					for (int x = 0; x < 14; x++) {
						tileMap [x, y] = 0;
						objectMap [x, y] = (GameObject)Instantiate (editorTile [0], new Vector2 (x + 1, y + 1), Quaternion.identity);
					}
				}
			}
		} else {
			tileMap = GameManager.GetComponent<CustomManager> ().tileMap;
			focus = true;
			for (int y = 4; y >= 0; y--) {
				for (int x = 0; x < 14; x++) {
					objectMap [x, y] = (GameObject)Instantiate (editorTile [tileMap [x, y]], new Vector2 (x + 1, y + 1), Quaternion.identity);
				}
			}
		}
	}

	void Update(){
		if (Input.GetMouseButton (0) && !playing) {
			Vector2 worldPos = Input.mousePosition;
			worldPos = Camera.main.ScreenToWorldPoint (worldPos);

			if (worldPos.x - (int)worldPos.x > 0.5f)
				worldPos.x = (int)worldPos.x + 1;
			if (worldPos.y - (int)worldPos.y > 0.5f)
				worldPos.y = (int)worldPos.y + 1;

			if (worldPos.x >= 1 && worldPos.x < 15 && worldPos.y >= 1 && worldPos.y < 6) {
				replaceTile ((int)worldPos.x - 1, (int)worldPos.y - 1, editorTile [currentTile]);
				hasEdited = true;
			}
		}
	}

	void replaceTile(int x, int y, GameObject tile) {
		//if placing dirt above grass
		if (currentTile == 2 && y > 0) {
			if (tileMap [x, y - 1] == 10) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[2], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 2;
			}
		}
		//if placing dirt above grass goal
		if (currentTile == 2 && y > 0) {
			if (tileMap [x, y - 1] == 11) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[6], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 6;
			}
		}
		//if placing dirt below air
		if (currentTile == 2 && y < 4) {
			if (tileMap [x, y + 1] == 3) {
				tile = editorTile [10];
				currentTile = 10;
			}
		}

		//if placing air above dirt
		if (currentTile == 3 && y > 0) {
			if (tileMap [x, y - 1] == 2) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[10], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 10;
			}
		}
		//if placing air above dirt goal
		if (currentTile == 3 && y > 0) {
			if (tileMap [x, y - 1] == 6) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[11], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 11;
			}
		}


		//if placing dirt goal above grass
		if (currentTile == 6 && y > 0) {
			if (tileMap [x, y - 1] == 10) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[2], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 2;
			}
		}
		//if placing dirt goal above grass goal
		if (currentTile == 6 && y > 0) {
			if (tileMap [x, y - 1] == 11) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[6], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 6;
			}
		}
		//if placing dirt goal below air
		if (currentTile == 6 && y < 4) {
			if (tileMap [x, y + 1] == 3 || tileMap[x, y + 1] == 7) {
				tile = editorTile [11];
				currentTile = 11;
			}
		}

		//if placing air goal above dirt
		if(currentTile == 7 && y > 0){
			if (tileMap [x, y - 1] == 2) {
				DestroyObject (objectMap [x, y - 1]);
				objectMap [x, y - 1] = (GameObject)Instantiate (editorTile[10], new Vector2 (x + 1, y), Quaternion.identity);
				tileMap [x, y - 1] = 10;
			}
		}


		if (currentTile == 8) {
			if (!focus) {
				if (tileMap [x, y] == 4) {
					tileMap [x, y] = 8;
					DestroyObject (objectMap [x, y]);
					objectMap [x, y] = (GameObject)Instantiate (editorTile [8], new Vector2 (x + 1, y + 1), Quaternion.identity);
					focus = true;
				} else if (tileMap [x, y] == 5) {
					tileMap [x, y] = 9;
					DestroyObject (objectMap [x, y]);
					objectMap [x, y] = (GameObject)Instantiate (editorTile [9], new Vector2 (x + 1, y + 1), Quaternion.identity);
					focus = true;
				}
				return;
			} else {
				for (int mapX = 0; mapX < 14; mapX++) {
					for (int mapY = 0; mapY < 5; mapY++) {
						if (tileMap [mapX, mapY] == 8) {
							tileMap [mapX, mapY] = 4;
							DestroyObject (objectMap [mapX, mapY]);
							objectMap [mapX, mapY] = (GameObject)Instantiate (editorTile [4], new Vector2 (mapX + 1, mapY + 1), Quaternion.identity);
						} else if (tileMap [mapX, mapY] == 9) {
							tileMap [mapX, mapY] = 5;
							DestroyObject (objectMap [mapX, mapY]);
							objectMap [mapX, mapY] = (GameObject)Instantiate (editorTile [5], new Vector2 (mapX + 1, mapY + 1), Quaternion.identity);
						}
					}
				}
				if (tileMap [x, y] == 4) {
					tileMap [x, y] = 8;
					DestroyObject (objectMap [x, y]);
					objectMap [x, y] = (GameObject)Instantiate (editorTile [8], new Vector2 (x + 1, y + 1), Quaternion.identity);
					focus = true;
				} else if (tileMap [x, y] == 5) {
					tileMap [x, y] = 9;
					DestroyObject (objectMap [x, y]);
					objectMap [x, y] = (GameObject)Instantiate (editorTile [9], new Vector2 (x + 1, y + 1), Quaternion.identity);
					focus = true;
				}
				return;
			}
		}

		if (tileMap [x, y] == 8 || tileMap [x, y] == 9)
			focus = false;

		if (tileMap [x, y] != currentTile) {
			DestroyObject (objectMap [x, y]);
			objectMap [x, y] = (GameObject)Instantiate (tile, new Vector2 (x + 1, y + 1), Quaternion.identity);
			tileMap [x, y] = currentTile;
		}

		if (currentTile == 10)
			currentTile = 2;
		if(currentTile == 11)
			currentTile = 6;
	}

	void OnGUI(){

		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = Screen.height / 20;

		if (!playing) {
			if (GUI.Button (new Rect (0, 0, Screen.width / 3, Screen.height / 4), "Save Map", customButton)) {
				Save ();
			}
			if (focus) {
				if (GUI.Button (new Rect (Screen.width / 3, 0, Screen.width / 3, Screen.height / 4), "Play Map\n(Set Par)", customButton)) {
					playing = true;
				}
			} else {
				GUI.Button (new Rect (Screen.width / 3, 0, Screen.width / 3, Screen.height / 4), "No Start Selected", customButton);
			}
			if (GUI.Button (new Rect (2 * Screen.width / 3, 0, Screen.width / 3, Screen.height / 4), "Back to Menu", customButton)) {
				GameManager.GetComponent<CustomManager> ().clear ();
				UnityEngine.SceneManagement.SceneManager.LoadScene (2);
			}

			if (GUI.Button (new Rect (0, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [0]))
				currentTile = 1;
			if (GUI.Button (new Rect (Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [1]))
				currentTile = 2;
			if (GUI.Button (new Rect (2 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [2]))
				currentTile = 3;
			if (GUI.Button (new Rect (3 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [3]))
				currentTile = 4;
			if (GUI.Button (new Rect (4 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [4]))
				currentTile = 5;
			if (GUI.Button (new Rect (5 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [5]))
				currentTile = 6;
			if (GUI.Button (new Rect (6 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [6]))
				currentTile = 7;
			if (GUI.Button (new Rect (7 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "", buttonTile [7]))
				currentTile = 8;

			GUI.Button (new Rect (0, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (2 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (3 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (4 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (5 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (6 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");
			GUI.Button (new Rect (7 * Screen.width / 8, Screen.height - Screen.width / 8, Screen.width / 8, Screen.width / 8), "");

		} else {
			if (!drawPlayTiles) {
				Play ();
				drawPlayTiles = true;
			}
		}
	}

	public void Save() {

		file = GameManager.GetComponent<CustomManager> ().file;

		if (!GameManager.GetComponent<CustomManager> ().fileExists ()) {

			FileStream tempFile = new FileStream (file, FileMode.Create);
			tempFile.Close ();
		}

		StreamWriter mapFile = new StreamWriter (file);

		for (int y = 4; y >= 0; y--) {
			for (int x = 0; x < 14; x++) {
				mapFile.Write (tileMap [x, y] + " ");
			}
			mapFile.Write ("\n");
		}
		if (!hasEdited)
			mapFile.WriteLine (GameManager.GetComponent<CustomManager> ().par);
		else
			mapFile.WriteLine ("0");

		mapFile.Close ();
	}

	void Play() {
		GameManager.GetComponent<CustomManager> ().tileMap = tileMap;

		Instantiate (goalTracker);
		levelCamera.transform.position = new Vector3 (levelCamera.transform.position.x, levelCamera.transform.position.y + 0.5f, levelCamera.transform.position.z);
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
