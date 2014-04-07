using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		Debug.Log(other.gameObject.name);
		Destroy(gameObject);
	}
}
