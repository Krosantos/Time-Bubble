using UnityEngine;
using System.Collections;

public class FreezeBase : MonoBehaviour {
	
	public bool isFrozen;
	Rigidbody2D self;
	Vector3 velocity;
	Vector3 location;
	void Start(){
		isFrozen = true;
		self = gameObject.rigidbody2D;
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
			self.gravityScale = 1;
			self.velocity = velocity;
		}
	}

	void Update(){
		if(isFrozen){
			self.gravityScale = 0;
			transform.position = location;
			Debug.Log ("I AM FROZEN");
		}
	}
}
