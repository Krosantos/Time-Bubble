using UnityEngine;
using System.Collections;

public class LevelCountToText : MonoBehaviour {

	TextMesh displaytext;

	// Use this for initialization
	void Start () {
		displaytext = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if (displaytext == null){
			guiText.text = ScoreManager.currentlevel.ToString();
		}else{
			displaytext.text = ScoreManager.currentlevel.ToString();
		}
	}
}
