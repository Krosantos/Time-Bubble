using UnityEngine;
using System.Collections;

public class A_Test : MonoBehaviour {

	public GameObject StartNode, EndNode, temp;

	// Use this for initialization
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
		temp = gameObject.GetComponent<A_Star>().AStar (StartNode,EndNode);
		//Debug.Log (temp);
		}
	}
}
