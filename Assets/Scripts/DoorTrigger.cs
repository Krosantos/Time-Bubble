using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			if (c.gameObject.GetComponent<MovePlayer>().hasKey == true) Application.LoadLevel(Application.loadedLevel);
		}
	}
}
