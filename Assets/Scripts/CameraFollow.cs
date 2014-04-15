using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject player;

	public float accel = 10f;

	[HideInInspector]
	public Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * accel);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
	}
}
