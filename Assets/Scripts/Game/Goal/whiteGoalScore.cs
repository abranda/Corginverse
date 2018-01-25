using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteGoalScore : MonoBehaviour {

	public int scored;

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "BlackPlayer") {
			scored = 1;
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "BlackPlayer") {
			scored = 0;
		}
	}
}
