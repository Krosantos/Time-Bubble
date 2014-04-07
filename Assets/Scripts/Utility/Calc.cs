using UnityEngine;
using System.Collections;

public class Calc : MonoBehaviour {

	public static bool isInRange(GameObject self, GameObject target, float maxDistance){
		/*Compares the distance between a given target and the calling
	 * instance, returning true if this value is less than the max distance specified.*/
		Vector3 position = self.transform.position;
		Vector3 difference = target.transform.position-position;
		float distance = difference.sqrMagnitude;
		if(distance<maxDistance) return true;
		else return false;
	}

	public static float getRangeFrom(GameObject self, GameObject target){
		Vector3 position = self.transform.position;
		Vector3 difference = target.transform.position-position;
		float distance = difference.sqrMagnitude;
		return distance;
	}
}
