using UnityEngine;
using System.Collections;

public class LoadLevelOnPress : MonoBehaviour {

	public string SceneToLoad;
	public KeyCode button = KeyCode.Space;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(button)){
			Application.LoadLevel(SceneToLoad);
		}
	}
}
