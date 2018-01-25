using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour {

	public GameObject particlePrefab;
	GameObject particle;

	void Start(){
		InvokeRepeating("CheckParticle", 0, 2);
	}

	void CheckParticle(){
		if (particle != null) {
			Destroy (particle);
		} else {
			if (Random.Range (0, 10) == 1) {
				particle = (GameObject)Instantiate (particlePrefab, this.transform);
			}
		}
	}
}
