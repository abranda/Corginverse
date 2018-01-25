using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCollisionDetection : MonoBehaviour {

	public bool collided;
	public bool grey;

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "BlackTile" || coll.gameObject.tag == "BlackGoal") {
			collided = false;
			GetComponentInParent<BlackSpawn> ().canSpawn = false;
		} else if (coll.gameObject.tag == "GreyTile") {
			collided = false;
			grey = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "BlackTile" || coll.gameObject.tag == "BlackGoal") {
			collided = true;
		} else if (coll.gameObject.tag == "GreyTile") {
			grey = true;
			collided = true;
		}
	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "BlackTile" || coll.gameObject.tag == "BlackGoal") {
			collided = true;
		} else if (coll.gameObject.tag == "GreyTile") {
			grey = true;
			collided = true;
		}
	}
}