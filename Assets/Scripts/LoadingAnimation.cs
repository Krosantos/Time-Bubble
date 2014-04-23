using UnityEngine;
using System.Collections;

public class LoadingAnimation : MonoBehaviour {

	float delaytime = .1f;
	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
		StartCoroutine(LoadingAnim());
	}
	
	IEnumerator LoadingAnim(){
		for(;;){
			textMesh.text = "Loading";
			yield return new WaitForSeconds(delaytime);
			textMesh.text = "Loading.";
			yield return new WaitForSeconds(delaytime);
			textMesh.text = "Loading..";
			yield return new WaitForSeconds(delaytime);
			textMesh.text = "Loading...";
			yield return new WaitForSeconds(delaytime);
		}
	}
}
