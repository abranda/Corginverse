using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public Animator Corgi;

	public bool White;

	void Update() {
		
		if (White) {
			if (GetComponent<WhiteMovement> ().left) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else if (GetComponent<WhiteMovement> ().right) {
				GetComponent<SpriteRenderer> ().flipX = false;
			}

			if (Corgi.GetCurrentAnimatorStateInfo (0).IsName ("Idle") || Corgi.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
				GetComponent<WhiteMovement> ().canJump = true;
				GetComponent<WhiteSpawn> ().canSpawn = true;
			} else {
				GetComponent<WhiteMovement> ().canJump = false;
				GetComponent<WhiteSpawn> ().canSpawn = false;
			}

			if (!GetComponent<WhiteMovement> ().jumping && !GetComponent<WhiteMovement> ().falling && (GetComponent<WhiteMovement>().left || GetComponent<WhiteMovement>().right)) {
				Corgi.SetBool ("Walk", true);
			} else if (GetComponent<WhiteMovement> ().jumping && !(GetComponent<WhiteMovement> ().bottomLeftCollider.collided || GetComponent<WhiteMovement> ().bottomRightCollider.collided)) {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", true);
			} else if (GetComponent<WhiteMovement> ().falling && !(GetComponent<WhiteMovement> ().bottomLeftCollider.collided || GetComponent<WhiteMovement> ().bottomRightCollider.collided)) {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", false);
				Corgi.SetBool ("Fall", true);
			} else {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", false);
				Corgi.SetBool ("Fall", false);
			}

		} else {
			if (GetComponent<BlackMovement> ().left) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else if (GetComponent<BlackMovement> ().right) {
				GetComponent<SpriteRenderer> ().flipX = false;
			}

			if (Corgi.GetCurrentAnimatorStateInfo (0).IsName ("Idle") || Corgi.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
				GetComponent<BlackMovement> ().canJump = true;
				GetComponent<BlackSpawn> ().canSpawn = true;
			} else {
				GetComponent<BlackMovement> ().canJump = false;
				GetComponent<BlackSpawn> ().canSpawn = false;
			}

			if (!GetComponent<BlackMovement> ().jumping && !GetComponent<BlackMovement> ().falling && (GetComponent<BlackMovement>().left || GetComponent<BlackMovement>().right)) {
				Corgi.SetBool ("Walk", true);
			} else if (GetComponent<BlackMovement> ().jumping && !(GetComponent<BlackMovement> ().bottomLeftCollider.collided || GetComponent<BlackMovement> ().bottomRightCollider.collided)) {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", true);
			} else if (GetComponent<BlackMovement> ().falling && !(GetComponent<BlackMovement> ().bottomLeftCollider.collided || GetComponent<BlackMovement> ().bottomRightCollider.collided)) {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", false);
				Corgi.SetBool ("Fall", true);
			} else {
				Corgi.SetBool ("Walk", false);
				Corgi.SetBool ("Jump", false);
				Corgi.SetBool ("Fall", false);
			}
		}
	}

	public void Spawn(){
		Corgi.SetBool ("Walk", false);
		Corgi.SetBool ("Jump", false);
		Corgi.SetBool ("Fall", false);
		Corgi.SetBool ("Spawn", true);

		if (White) {
			GetComponent<WhiteMovement> ().canMove = false;
		} else {
			GetComponent<BlackMovement> ().canMove = false;
		}
	}
}
