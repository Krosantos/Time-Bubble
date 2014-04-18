using UnityEngine;
using System.Collections;

public class LevelCountToText : MonoBehaviour {

	TextMesh displaytext;

	// Use this for initialization
	void Start () {
		displaytext = GetComponent<TextMesh>();
		if (displaytext == null){
			guiText.text = "Level "+ScoreManager.currentlevel.ToString();
		}else{
			displaytext.text = "Level "+ScoreManager.currentlevel.ToString();
		}
	}
}
