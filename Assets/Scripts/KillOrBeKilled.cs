using UnityEngine;
using System.Collections;

public class KillOrBeKilled : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			if (gameObject.GetComponent<MobBase>().accelMod < .2f){
				Destroy(gameObject);
			}else{
				Destroy(c.gameObject);
			}
		}
	}
}
