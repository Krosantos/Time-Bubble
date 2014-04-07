using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if(other.gameObject.GetComponent<TestMob>() != null){
			other.gameObject.GetComponent<TestMob>().health -= 1;
		}
		Debug.Log(other.gameObject.name);
		Destroy(gameObject);
	}
}
