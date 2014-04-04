using UnityEngine;
using System.Collections;

public class FreezeBase : MonoBehaviour {
	
	public bool isFrozen;
	Rigidbody self;
	Vector3 velocity;
	Vector3 location;
	void Start(){
		//isFrozen = true;
		self = gameObject.rigidbody;
		location = transform.position;
		velocity = self.velocity;

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
			self.useGravity = true;
			self.velocity = velocity;
		}
	}

	void Update(){
		if(isFrozen){
			self.useGravity = false;
			transform.position = location;
			Debug.Log ("I AM FROZEN");
		}
	}
}
