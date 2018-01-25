using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPar : MonoBehaviour {

	int[] par = new int[] {
		1, //Level  1
		1, //Level  2
		1, //Level  3
		2, //Level  4
		2, //Level  5
		3, //Level  6
		3, //Level  7
		4, //Level  8

		4, //Level  9
		5, //Level 10
		5, //Level 11
		4, //Level 12
		6, //Level 13
		4, //Level 14
		6, //Level 15
		6, //Level 16

		5, //Level 17
		7, //Level 18
		4, //Level 19
		6, //Level 20
		6, //Level 21
		5, //Level 22
		5, //Level 23
		5, //Level 24

		3, //Level 25
		6, //Level 26
		7, //Level 27
		5, //Level 28
		5, //Level 29
		11,//Level 30
		4, //Level 31
		8, //Level 32

		7, //Level 33
		7, //Level 34
		4, //Level 35
		6, //Level 36
		6, //Level 37
		4, //Level 38
		7, //Level 39
		9  //Level 40

	};

	void Start(){
		DontDestroyOnLoad (this.gameObject);
	}

	public int getPar(int level){
		return par [level];
	}
}
