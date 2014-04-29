using UnityEngine;
using System.Collections;

public class DynamicZOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (renderer.bounds.min).y * -1;
	}
}
