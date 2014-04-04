using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour{

	public float gravity = -25f;
	public float speed = 10f;
	public float jumpspeed = 3f;
	public float accel = .5f;
	public float jumplength = .5f;
	public Transform groundchecker;
	public Transform wallchecker;
	public LayerMask whatisground;
	
	private Vector3 velocity;
	private float xmove;
	private float jumptimer;

	public KeyCode up, down, left, right;

	// Use this for initialization
	void Awake () {
		jumptimer = jumplength;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = rigidbody2D.velocity;
		xmove = 0f;

		if (IsGrounded()) velocity.y = 0f;

		if (Input.GetKeyDown(up) && IsGrounded()){
			velocity.y = Mathf.Sqrt(2f * jumpspeed * -gravity);
			jumptimer = 0f;
		}else if(Input.GetKey(up) && jumptimer < jumplength){
			velocity.y = Mathf.Sqrt(2f * jumpspeed * -gravity);
			jumptimer += Time.deltaTime;
		}

		if(Input.GetKeyUp(up) && jumptimer < jumplength){
			jumptimer = jumplength;
		}

		if (Input.GetKey(down) && !IsGrounded()){
			velocity.y = gravity;
		}
		if (Input.GetKey(left)){
			if (transform.localScale.x > 0f) ReverseXScale();
			if (!IsFacingWall()) xmove = -1f;
		}
		else if (Input.GetKey(right)){
			if (transform.localScale.x < 0f) ReverseXScale();
			if (!IsFacingWall()) xmove = 1f;
		}

		velocity.x = Mathf.Lerp(velocity.x, xmove * speed, Time.deltaTime * accel);

		velocity.y += gravity * Time.deltaTime;

		rigidbody2D.velocity = velocity;
	}

	private bool IsGrounded(){
		return Physics2D.OverlapCircle(groundchecker.position, 0.1f, whatisground);
	}

	private bool IsFacingWall(){
		return Physics2D.OverlapCircle(wallchecker.position, 0.2f, whatisground);
	}

	private void ReverseXScale(){
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
}
