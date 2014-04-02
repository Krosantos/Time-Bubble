using UnityEngine;
using System.Collections;

public class FreezeBase : MonoBehaviour {
	
	public bool isFrozen = false;
	Rigidbody2D self;
	Vector3 velocity;
	Vector3 location;
	void Start(){
		self = gameObject.rigidbody2D;
	}

	public void Freeze(){
		if(!isFrozen){
			Debug.Log ("WARK");
			isFrozen = true;
			location = transform.position;
			velocity = self.velocity;
		}
	}

	public void Unfreeze(){
		if(isFrozen){
			isFrozen = false;
			self.velocity = velocity;
		}
	}

	void Update(){
		if(isFrozen){
			transform.position = location;
			Debug.Log ("I AM FROZEN");
		}
	}
}
