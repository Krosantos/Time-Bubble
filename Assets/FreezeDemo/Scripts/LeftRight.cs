using UnityEngine;
using System.Collections;

public class LeftRight : MonoBehaviour {

	Vector2 speed = new Vector2 (-4,0);
	Rigidbody2D self;
	bool isGoingLeft = false;

	void Start(){
		self = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update () {

		if(transform.position.x >-7){
			speed.x = -4;
		}

		if(transform.position.x <= -30){
			speed.x = 4;
		}

		self.velocity = speed;

		if(transform.position.y != 0.3997101f){
			transform.position = new Vector2(transform.position.x, 0.3997101f);
		}
	}
}
