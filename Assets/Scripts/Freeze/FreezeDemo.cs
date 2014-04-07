using UnityEngine;
using System.Collections;

public class FreezeDemo : MonoBehaviour {

	public Vector3 speed = new Vector3(0,2,0);
	public float rayLength = 250f;
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		
		// Casts the ray and get the first game object hit
		if (Physics.Raycast (ray, out hit, rayLength)){
			Vector3 target = new Vector3(hit.point.x, hit.point.y, 0f);
			transform.position = target;
		}

		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += (speed*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.position -= (speed*Time.deltaTime);
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel("Freeze Demo");
		}
	}
}
