using UnityEngine;
using System.Collections;

public class FreezeDemo : MonoBehaviour {

	public Vector3 speed = new Vector3(0,2,0);

	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += (speed*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.position -= (speed*Time.deltaTime);
		}
	}
}
