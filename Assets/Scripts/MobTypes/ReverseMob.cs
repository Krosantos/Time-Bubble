using UnityEngine;
using System.Collections;

public class ReverseMob : MobBase {

	public Sprite sleep, awake;
	private bool primed =false;

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

	public override void Update (){
		
		
		if (accelMod <= 0f){
			if (!isPetrified && primed) AudioManager.instance.Play(AudioManager.instance.petrifysound);
			isPetrified = true;
			primed = true;
			gameObject.layer = 13;
		}else{
			isPetrified = false;
			gameObject.layer = 11;
		}
		
		//Check for death
		gradColor = Color.Lerp (tintcolor, petColor,1-accelMod);
		if(isPetrified){
			gameObject.renderer.material.color = Color.black;
			if (GetComponent<Animator>()) GetComponent<Animator>().speed = 0f;
		}
		else{
			gameObject.renderer.material.color = gradColor;
			if (GetComponent<Animator>()) GetComponent<Animator>().speed = Random.Range(.5f,1f);
		}
		
		//Determine movetarget
		switch (AIState){
		case AIStates.Idle:
			Idle ();
			break;
		case AIStates.Pursue:
			Pursue();
			break;
		case AIStates.Attack:
			Pursue();
			break;
		}
		Move ();
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
