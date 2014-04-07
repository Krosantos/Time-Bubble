using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "EnemyActivator"){
			GetComponent<TestMob>().enabled = true;
		}
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "EnemyActivator"){
			GetComponent<TestMob>().enabled = false;
		}
	}
}
