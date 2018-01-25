using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFloat : MonoBehaviour {

	Vector2 StartPosition;
	public float FloatSpeed;

	void Start () {
		StartPosition = this.transform.position;
		if (Random.Range (0, 10) < 5)
			FloatSpeed = -FloatSpeed;
	}

	void Update () {
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + FloatSpeed);

		if (this.transform.position.y - StartPosition.y > 0.25f)
			FloatSpeed = -FloatSpeed;
		if (this.transform.position.y - StartPosition.y < -0.25f)
			FloatSpeed = -FloatSpeed;
	}
}
