using UnityEngine;
using System.Collections;

public class KeyGUI : MonoBehaviour {

	MovePlayer m;

	void Start(){
		m = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
	}

	// Update is called once per frame
	void Update () {
		if (m.hasKey) guiTexture.enabled = true;
	}
}
