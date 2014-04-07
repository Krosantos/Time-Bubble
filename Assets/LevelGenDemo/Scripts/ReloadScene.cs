using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	public KeyCode key = KeyCode.Space;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(key)) Application.LoadLevel(Application.loadedLevel);
	}
}
