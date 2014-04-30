using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	public KeyCode key;

	void Start(){
		if (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer){
			gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key)){
			Application.Quit();
		}
	}
}
