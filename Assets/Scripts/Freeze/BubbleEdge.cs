using UnityEngine;
using System.Collections;

public class BubbleEdge : MonoBehaviour {

	public FreezeBase target;

	void OnTriggerEnter(Collider other){
		if(other.GetComponent<FreezeBase>() != null){
			other.GetComponent<FreezeBase>().Unfreeze();
			//Debug.Log ("Boop!");
		}
		else{
			//Debug.Log ("False Alarm!");
		}

		if(other.GetComponent<SlowBase>() != null){
			other.GetComponent<SlowBase>().Slow();
		}

	}

	void OnTriggerExit(Collider other){
		if(other.GetComponent<FreezeBase>() != null){
			other.GetComponent<FreezeBase>().Freeze();
		}

		if(other.GetComponent<SlowBase>() != null){
			other.GetComponent<SlowBase>().Unslow();
		}
	}
}
