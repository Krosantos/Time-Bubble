using UnityEngine;
using System.Collections;

public class SlowOrb : MonoBehaviour {

	public Vector3 direction, moveVec;
	public float speed;

	void Start(){
		//Eventually time out the projectile
		GameObject.Destroy(gameObject, 15f);
	}

	void Update(){
		moveVec = direction.normalized*speed*Time.deltaTime;
		gameObject.GetComponent<Rigidbody>().MovePosition (moveVec+transform.position);
	}

	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag == "Player"){
			coll.gameObject.GetComponent<MovePlayer>().slowEffect();
		}
		GameObject.Destroy (gameObject);
	}
}
