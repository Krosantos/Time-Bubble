using UnityEngine;
using System.Collections;

public class KeyTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			c.gameObject.GetComponent<MovePlayer>().hasKey = true;
		}
	}
}
