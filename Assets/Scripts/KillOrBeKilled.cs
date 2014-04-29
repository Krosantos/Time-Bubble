using UnityEngine;
using System.Collections;

public class KillOrBeKilled : MonoBehaviour {

	GUITexture camfadetex;
	float alpha = 0f;

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player"){
			if (gameObject.GetComponent<MobBase>().isPetrified){
				clearNode();
				ScreenShake2D.Shake(.25f,1f);
				AudioManager.instance.Play(AudioManager.instance.destroymob);
				Destroy(gameObject);
			}else{
				camfadetex = GameObject.FindGameObjectWithTag("CamFade").guiTexture;
				c.gameObject.GetComponent<MovePlayer>().enabled = false;
				c.gameObject.GetComponentInChildren<Light2D>().enabled = false;
				StartCoroutine(EndGame());
			}
		}
	}

	void clearNode(){
		GameObject deathNode = gameObject.GetComponent<MobBase>().lockNode();
		if(deathNode != null){
			deathNode.GetComponent<Node>().turnOn();
		}
	}

	IEnumerator EndGame(){
		Time.timeScale = .5f;
		ScreenShake2D.Shake(.25f,.25f);
		yield return new WaitForSeconds(.5f);
		while (alpha < 1f){
			alpha += Time.deltaTime;
			Mathf.Clamp01(alpha);
			camfadetex.color = new Color(0f,0f,0f,alpha);
			yield return 0;
		}	
		Time.timeScale = 1f;
		Application.LoadLevel("End");
	}
	
}
