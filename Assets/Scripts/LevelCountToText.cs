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
		displaytext.text = ScoreManager.currentlevel.ToString();
	}
}
