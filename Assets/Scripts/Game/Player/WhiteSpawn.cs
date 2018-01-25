using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSpawn : MonoBehaviour {

	public GameObject goalTracker;
	GameObject focusArrow;
	public bool CustomMap;

	public bool canSpawn;

	public GameObject spawnable;
	public GameObject underDog;
	public WhiteCollisionDetection bottom;
	GameObject spawned;
	bool youWon;

	bool spawnLimit = false;

	void Start(){
		goalTracker = GameObject.FindGameObjectWithTag ("GoalTracker");
	}

	void Update(){
		if (!spawnLimit) {
			if (GetComponent<InFocus> ().isFocus) {
				if (focusArrow == null) {
					focusArrow = GameObject.FindGameObjectWithTag ("InFocus");
				}
			}
		} else {
			if (focusArrow != null)
				Destroy (focusArrow);
		}

		if (isMainGame ()) {
			if (goalTracker.GetComponent<goalCount> ().youWon)
				youWon = true;
			else
				youWon = false;
		} else if (goalTracker.GetComponent<GoalCountEditor> () != null) {
			if (goalTracker.GetComponent<GoalCountEditor> ().youWon)
				youWon = true;
			else
				youWon = false;
		}

		if (spawned != null) {

			if (spawned.GetComponent<Animator> ().GetBool ("Ended")) {

				GetComponent<WhiteMovement> ().canMove = true;

				GameObject temp = spawned;

				spawned = (GameObject)Instantiate (spawnable);
				spawned.GetComponent<BlackSpawn> ().CustomMap = CustomMap;
				spawned.transform.position = new Vector2 (transform.position.x, transform.position.y + 1);
				spawned.GetComponent<InFocus> ().isFocus = true;
				spawned.GetComponent<SpriteRenderer> ().flipX = !GetComponent<SpriteRenderer> ().flipX;
				GetComponent<AnimationManager> ().Corgi.SetBool ("Spawn", false);
				GetComponent<WhiteMovement> ().canMove = true;
				GetComponent<WhiteMovement> ().canMoveLeft = true;
				GetComponent<WhiteMovement> ().canMoveRight = true;
				this.enabled = false;
				DestroyImmediate (temp);

			} else if (spawned.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Divide Bottom")) {
				spawned.GetComponent<Animator> ().SetBool ("Started", true);
			} else if (!spawned.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Divide Bottom") && spawned.GetComponent<Animator> ().GetBool ("Started")) {
				spawned.GetComponent<Animator> ().SetBool ("Ended", true);
			}
		}
	}

	bool isMainGame(){
		if (goalTracker.GetComponent<goalCount> () != null) {
			if (goalTracker.GetComponent<goalCount> ().spawned > 29)
				spawnLimit = true;
			return true;
		}
		if (goalTracker.GetComponent<GoalCountEditor> ().spawned > 29)
			spawnLimit = true;
		return false;
	}

	void OnGUI(){

		if (GetComponent<InFocus> ().isFocus && !youWon) {
			
			GUIStyle customButton = new GUIStyle ("button");
			customButton.fontSize = Screen.height / 20;
			if (bottom.grey || GetComponent<WhiteMovement> ().left || GetComponent<WhiteMovement> ().right || (GetComponent<WhiteMovement> ().jumping || GetComponent<WhiteMovement> ().falling)) {
				canSpawn = false;
				if (focusArrow != null) {
					focusArrow.GetComponent<SpriteRenderer> ().enabled = false;
				}
			} else {
				if (focusArrow != null) {
					focusArrow.GetComponent<SpriteRenderer> ().enabled = true;
				}
			}
			if (canSpawn && !GetComponent<WhiteMovement> ().jumping && !GetComponent<WhiteMovement> ().falling && !spawnLimit) {
				GUI.contentColor = new Color (127, 127, 127);
				if (CustomMap) {
					if (GUI.Button (new Rect (Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 2, Screen.height / 6), "Corgi Split", customButton)) {
						Spawn ();
					}
					if (!goalTracker.GetComponent<GoalCountEditor> ().play) {
						GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Par Set: " + goalTracker.GetComponent<GoalCountEditor> ().spawned, customButton);
					} else {
						GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Par: " + goalTracker.GetComponent<GoalCountEditor> ().spawned + "/" + goalTracker.GetComponent<GoalCountEditor> ().par, customButton);
					}
				} else {
					GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Level " + goalTracker.GetComponent<goalCount>().level + "\n\nPar: " + goalTracker.GetComponent<goalCount> ().spawned + "/" + goalTracker.GetComponent<goalCount> ().par, customButton);
					if (GUI.Button (new Rect (Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 2, Screen.height / 6), "Corgi Split", customButton)) {
						Spawn ();
					}
				}
			} else {
				GUI.contentColor = Color.gray;
				if (CustomMap) {
					GUI.Button (new Rect (Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 2, Screen.height / 6), "Corgi Split", customButton);
					if (!goalTracker.GetComponent<GoalCountEditor> ().play) {
						GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Par Set: " + goalTracker.GetComponent<GoalCountEditor> ().spawned, customButton);
					} else {
						GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Par: " + goalTracker.GetComponent<GoalCountEditor> ().spawned + "/" + goalTracker.GetComponent<GoalCountEditor> ().par, customButton);
					}
				} else {
					GUI.Button (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 4), "Level " + goalTracker.GetComponent<goalCount>().level + "\n\nPar: " + goalTracker.GetComponent<goalCount> ().spawned + "/" + goalTracker.GetComponent<goalCount> ().par, customButton);
					GUI.Button (new Rect (Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 2, Screen.height / 6), "Corgi Split", customButton);
				}
			}

			GUI.contentColor = new Color (127, 127, 127);
			if (CustomMap) {
				if (GameObject.FindGameObjectWithTag ("EditorObject") != null) {
					if (GUI.Button (new Rect (0, 0, Screen.width / 4, Screen.height / 4), "Back", customButton)) {
						UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
					}
					if (GUI.Button (new Rect ((Screen.width / 4) + (Screen.width / 2), 0, Screen.width / 4, Screen.height / 4), "Exit to Menu\n(Without Saving)", customButton)) {
						UnityEngine.SceneManagement.SceneManager.LoadScene (2);
					}
				} else {
					if (GUI.Button (new Rect (0, 0, Screen.width / 4, Screen.height / 4), "Restart", customButton)) {
						UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
					}
					if (GUI.Button (new Rect ((Screen.width / 4) + (Screen.width / 2), 0, Screen.width / 4, Screen.height / 4), "Level Select", customButton)) {
						UnityEngine.SceneManagement.SceneManager.LoadScene (1);
					}
				}
			} else {
				if (GUI.Button (new Rect (0, 0, Screen.width / 4, Screen.height / 4), "Restart", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
				}
				if (GUI.Button (new Rect ((Screen.width / 4) + (Screen.width / 2), 0, Screen.width / 4, Screen.height / 4), "Level Select", customButton)) {
					UnityEngine.SceneManagement.SceneManager.LoadScene (1);
				}
			}

		} else if(GetComponent<InFocus> ().isFocus && youWon) {
			GameObject.FindGameObjectWithTag ("InFocus").GetComponentInParent<InFocus> ().isFocus = false;
		}
	}


	void Spawn(){
		if (isMainGame ())
			goalTracker.GetComponent<goalCount> ().spawned++;
		else
			goalTracker.GetComponent<GoalCountEditor> ().spawned++;
		GetComponent<InFocus> ().isFocus = false;
		GetComponent<AnimationManager> ().Spawn ();
		spawned = (GameObject)Instantiate (underDog);
		spawned.transform.position = new Vector2 (transform.position.x, transform.position.y + 1);
		spawned.GetComponent<SpriteRenderer> ().flipY = true;
		if (!GetComponent<SpriteRenderer> ().flipX)
			spawned.GetComponent<SpriteRenderer> ().flipX = true;
	}
}