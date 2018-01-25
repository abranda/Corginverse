using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCollisionDetection : MonoBehaviour {

	public bool collided;
	public bool grey;

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "WhiteTile" || coll.gameObject.tag == "WhiteGoal") {
			collided = false;
			GetComponentInParent<WhiteSpawn> ().canSpawn = false;
		} else if (coll.gameObject.tag == "GreyTile") {
			collided = false;
			grey = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "WhiteTile" || coll.gameObject.tag == "WhiteGoal") {
			collided = true;
		} else if (coll.gameObject.tag == "GreyTile") {
			grey = true;
			collided = true;
		}
	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "WhiteTile" || coll.gameObject.tag == "WhiteGoal") {
			collided = true;
		} else if (coll.gameObject.tag == "GreyTile") {
			grey = true;
			collided = true;
		}
	}
}