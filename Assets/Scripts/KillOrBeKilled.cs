using UnityEngine;
using System.Collections;

public class KillOrBeKilled : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			if (gameObject.GetComponent<MobBase>().isPetrified){
				clearNode();
				ScreenShake2D.Shake(.25f,1f);
				AudioManager.instance.Play(AudioManager.instance.destroymob);
				Destroy(gameObject);
			}else{
				Application.LoadLevel("End");
			}
		}
	}

	void clearNode(){
		GameObject deathNode = gameObject.GetComponent<MobBase>().lockNode();
		if(deathNode != null){
			deathNode.GetComponent<Node>().turnOn();
		}
	}
}
