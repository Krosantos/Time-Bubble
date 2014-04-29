using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	bool isPaused = false;
	LightControl lightControl;

	void Start (){
		lightControl = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LightControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) isPaused = !isPaused;

		if (isPaused){
			Time.timeScale = .000000000000000001f;
			guiText.enabled = true;
			lightControl.enabled = false;
		}else{
			Time.timeScale = 1f;
			guiText.enabled = false;
			lightControl.enabled = true;
		}
	}
}
