using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomManager : MonoBehaviour {

	public int level;
	public string file;
	public int[,] tileMap;

	public int par;

	public bool fileExists(){
		file = Application.persistentDataPath + "/Custom" + level + ".map";

		if (File.Exists (file))
			return true;
		return false;
	}

	public bool fileExists(int level) {
		string tempFile = Application.persistentDataPath + "/Custom" + level + ".map";

		if (File.Exists (tempFile))
			return true;
		return false;
	}

	public bool hasSetPar(int level){

		string tempFile = Application.persistentDataPath + "/Custom" + level + ".map";

		StreamReader mapFile = new StreamReader (tempFile);

		for (int i = 0; i < 5; i++) {
			mapFile.ReadLine ();
		}
		par = int.Parse(mapFile.ReadLine ());

		if (par > 0)
			return true;
		return false;
	}

	public void clear(){
		level = 0;
		file = null;
		tileMap = null;
		par = 0;
	}
}
