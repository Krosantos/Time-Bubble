using UnityEngine;
using System.Collections;

public class ScreenShakeUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ScreenShake2D.SetShake(0f);
	}
	
	// Update is called once per frame
	void Update () {
		ScreenShake2D.Update();
	}
}
