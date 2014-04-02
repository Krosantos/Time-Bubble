using UnityEngine;
using System.Collections;

public class BubbleEdge : MonoBehaviour {

	public FreezeBase target;

	void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<FreezeBase>() != null){
			other.GetComponent<FreezeBase>().Unfreeze();
			Debug.Log ("Boop!");
		}
		else{
			Debug.Log ("False Alarm!");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.GetComponent<FreezeBase>() != null){
			other.GetComponent<FreezeBase>().Freeze();
		}
	}
}
