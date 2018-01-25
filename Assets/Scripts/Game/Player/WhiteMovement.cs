using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteMovement : MonoBehaviour {

	public WhiteCollisionDetection topCollider;
	public WhiteCollisionDetection bottomLeftCollider;
	public WhiteCollisionDetection bottomRightCollider;
	public WhiteCollisionDetection leftCollider;
	public WhiteCollisionDetection rightCollider;

	float xMod = 0;
	float yMod = 0;

	float speed = -3;
	public bool right;
	public bool left;

	float jumpSpeedSet = 5.1f;
	float jumpSpeed = 5.1f;
	float jumpDecay = 0.25f;
	public bool jumping;
	public bool falling;

	public bool canMove;
	public bool canMoveLeft;
	public bool canMoveRight;
	public bool canJump;


	void FixedUpdate(){

		if (!canMove) {
			canMoveLeft = false;
			canMoveRight = false;
		}

		if (jumping) {
			jump ();
		} else if (!bottomLeftCollider.collided || !bottomRightCollider.collided) {
			if (!falling) {
				falling = true;
				jumpSpeed = 0;
			}
			fall ();
		} else {
			falling = false;
			transform.position = new Vector2 (transform.position.x, (int)(transform.position.y + 0.25f));
		}

		transform.position = new Vector2 (transform.position.x + xMod, transform.position.y + yMod);

		xMod = 0;
		yMod = 0;

		if (topCollider.collided && (bottomLeftCollider.collided || bottomRightCollider.collided)) {
			if (leftCollider.collided) {
				xMod = speed * Time.deltaTime;
			} else if (rightCollider.collided) {
				xMod = -speed * Time.deltaTime;
			}
		}

		if (jumping || falling) {
			if (leftCollider.collided) {
				xMod = (speed / 6) * Time.deltaTime;
			} else if (rightCollider.collided) {
				xMod = -(speed / 6) * Time.deltaTime;
			}
		}
	}

	public void pupJump(){
		if ((bottomLeftCollider.collided || bottomRightCollider.collided) && !jumping && !falling) {
			if (canJump) {
				jumping = true;
				jumpSpeed = jumpSpeedSet;
			}
		}
	}

	public void pupRight(){
		if (canMoveRight && !rightCollider.collided) {
			xMod = speed * Time.deltaTime;
			left = false;
			right = true;
		}
	}

	public void pupLeft(){
		if (canMoveLeft && !leftCollider.collided) {
			xMod = -speed * Time.deltaTime;
			left = true;
			right = false;
		}
	}

	void jump(){
		if (topCollider.collided || jumpSpeed <= 0) {
			jumping = false;
			falling = true;
			jumpSpeed = 0;
		} else if (!topCollider.collided) {
			jumpSpeed -= jumpDecay;
			yMod += (-jumpSpeed * Time.deltaTime);
		}
	}

	void fall(){
		jumpSpeed -= jumpDecay;
		yMod += (-jumpSpeed * Time.deltaTime);
	}
}