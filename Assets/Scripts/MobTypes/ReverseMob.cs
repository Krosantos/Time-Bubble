using UnityEngine;
using System.Collections;

public class ReverseMob : MobBase {

	public Sprite sleep, awake;

	public override void Start ()
	{
		base.Start ();
		speed = 10f;
		health = 2f;
		detectRange = 40f;
		targetRange = 39f;
		engageRange = 5f;
		resistRate = 1.2f;
		regenRate = 0.1f;
		accel = 10f;
		accelMod = 0f;
	}

	protected override IEnumerator UnFreeze (){
		for(;;){
			if(accelMod > 0){
				accelMod -= regenRate*Time.deltaTime;
			}
			if(accelMod < 1){
				accelMod = 0;
				gameObject.GetComponent<SpriteRenderer>().sprite = sleep;
				Suicide("UnFreeze");
			}
			yield return 0;
		}
	}

	public override void Petrify (float compositeIntensity){
		StopCoroutine("UnFreeze");
		if(accelMod < 1f){
			accelMod+=compositeIntensity*Time.deltaTime*2f*resistRate;
		}
		if(accelMod > 0.55f){
			gameObject.GetComponent<SpriteRenderer>().sprite = awake;
		}
		if(accelMod >= 1f){
			accelMod = 1f;
		}
	}
}
