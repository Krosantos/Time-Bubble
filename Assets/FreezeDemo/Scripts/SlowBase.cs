using UnityEngine;
using System.Collections;

public class SlowBase : MonoBehaviour {

	public bool isSlowed;
	public float velMod = 0.5f;
	Rigidbody self;
	Vector3 velocity;
	void Start(){
		self = gameObject.rigidbody;
		velocity = self.velocity;
	}
	
	public void Slow(){
		if(!isSlowed){
			velocity = self.velocity;
			isSlowed = true;
		}
	}
	
	public void Unslow(){
		if(isSlowed){
			isSlowed = false;
		}
	}
	
	void Update(){
		if(isSlowed){
			self.velocity = velocity*velMod;
		}
		else{
			self.velocity = velocity;
		}
	}
}
