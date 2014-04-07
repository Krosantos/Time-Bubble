using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			GetComponent<TestMob>().enabled = true;
		}
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Player"){
			GetComponent<TestMob>().enabled = false;
		}
	}
}
