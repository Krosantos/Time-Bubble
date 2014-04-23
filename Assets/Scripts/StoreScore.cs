using UnityEngine;
using System.Collections;

public class StoreScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (ScoreManager.currentlevel > PlayerPrefs.GetInt("Best Score")){
			PlayerPrefs.SetInt("Best Score", ScoreManager.currentlevel);
		}
		GetComponent<TextMesh>().text = "Best: Level " + PlayerPrefs.GetInt("Best Score");
	}

}
