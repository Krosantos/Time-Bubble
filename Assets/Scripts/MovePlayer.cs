using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour{
	
	public float speed = 10f;
	public float jumpspeed = 3f;
	public float accel = .5f;
	
	public bool hasKey;

	private CharacterController controller;
	private Vector3 velocity;
	private float xmove;
	private float ymove;

	public KeyCode up, down, left, right;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		hasKey = false;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = controller.velocity;
		xmove = 0f;
		ymove = 0f;

		if (Input.GetKey(left)){
			if (transform.localScale.x > 0f) ReverseXScale();
			xmove = -1f;
		}
		if (Input.GetKey(right)){
			if (transform.localScale.x < 0f) ReverseXScale();
			xmove = 1f;
		}
		if (Input.GetKey(up)){
			ymove = 1f;
		}
		if (Input.GetKey(down)){
			ymove = -1f;
		}

		if ((Input.GetKey(up) || Input.GetKey(down)) && (Input.GetKey(left) || Input.GetKey(right) )){
			xmove *= 0.707f;
			ymove *= 0.707f;
		}

		velocity.x = Mathf.Lerp(velocity.x, xmove * speed, Time.deltaTime * accel);
		velocity.y = Mathf.Lerp(velocity.y, ymove * speed, Time.deltaTime * accel);

		controller.Move(velocity*Time.deltaTime);
	}

	private void ReverseXScale(){
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
}
