using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackGoalScore : MonoBehaviour {

	public int scored;

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "WhitePlayer") {
			scored = 1;
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "WhitePlayer") {
			scored = 0;
		}
	}
}
