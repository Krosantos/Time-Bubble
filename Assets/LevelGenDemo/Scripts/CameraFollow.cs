using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject player;

	public float accel = 10f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * accel);
		transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
	}
}
