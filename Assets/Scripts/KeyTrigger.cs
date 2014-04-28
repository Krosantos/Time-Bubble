using UnityEngine;
using System.Collections;

public class KeyTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			if (!c.gameObject.GetComponent<MovePlayer>().hasKey){
				c.gameObject.GetComponent<MovePlayer>().hasKey = true;
				AudioManager.instance.Play(AudioManager.instance.pickupkey);
				Destroy(gameObject);
			}
		}
	}
}
