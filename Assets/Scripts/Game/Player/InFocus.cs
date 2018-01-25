using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFocus : MonoBehaviour {

	public bool isFocus;
	public GameObject FocusPrefab;
	GameObject Focus;

	void Update () {

		if (isFocus && Focus == null) {
			Focus = (GameObject)Instantiate (FocusPrefab, this.transform);
			if(GetComponent<AnimationManager>().White)
				Focus.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.75f);
			else
				Focus.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.75f);
			Focus.transform.parent = this.transform;
		} else if(!isFocus && Focus != null){
			Destroy (Focus);
		}

	}
}
