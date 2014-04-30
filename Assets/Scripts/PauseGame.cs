using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	bool isPaused = false;
	LightControl lightControl;
	Transform child;

	void Start (){
		lightControl = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LightControl>();
		child = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) isPaused = !isPaused;

		if (isPaused){
			AudioListener.pause = true;
			Time.timeScale = .000000000000000001f;
			guiText.enabled = true;
			lightControl.enabled = false;
			child.guiText.enabled = true;
		}else{
			AudioListener.pause = false;
			Time.timeScale = 1f;
			guiText.enabled = false;
			lightControl.enabled = true;
			child.guiText.enabled = false;
		}
	}
}
