using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour{

	public float gravity = -25f;
	public float speed = 10f;
	public float jumpspeed = 3f;
	public float accel = .5f;
	public float jumplength = .5f;

	private CharacterController2D controller;
	private Vector3 velocity;
	private float xmove;
	private float jumptimer;

	public KeyCode up, down, left, right;

	// Use this for initialization
	void Awake () {
		controller = GetComponent<CharacterController2D>();
		jumptimer = jumplength;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = controller.velocity;

		if (controller.isGrounded) velocity.y = 0f;

		if (Input.GetKeyDown(up) && controller.isGrounded){
			velocity.y = Mathf.Sqrt(2f * jumpspeed * -gravity);
			jumptimer = 0f;
		}else if(Input.GetKey(up) && jumptimer < jumplength){
			velocity.y = Mathf.Sqrt(2f * jumpspeed * -gravity);
			jumptimer += Time.deltaTime;
		}

		if (Input.GetKey(down) && !controller.isGrounded){
			velocity.y = gravity;
		}
		if (Input.GetKey(left)){
			xmove = -1f;
			if (transform.localScale.x > 0f) ReverseXScale();
		}
		else if (Input.GetKey(right)){
			xmove = 1f;
			if (transform.localScale.x < 0f) ReverseXScale();
		}else xmove = 0f;

		velocity.x = Mathf.Lerp(velocity.x, xmove * speed, Time.deltaTime * accel);

		velocity.y += gravity * Time.deltaTime;

		controller.move(velocity * Time.deltaTime);
	}

	private void ReverseXScale(){
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
}
