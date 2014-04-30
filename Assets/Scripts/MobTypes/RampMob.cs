using UnityEngine;
using System.Collections;

public class RampMob : MobBase {


	public override void Start ()
	{
		base.Start();
		speed = 6f;
		health = 2f;
		detectRange = 20f;
		targetRange = 19f;
		engageRange = 5f;
		resistRate = 1f;
		regenRate = 0.2f;
		accel = 10f;
	}
	
	protected override IEnumerator UnFreeze (){
		recoverTime = 0;
		float t = 1f;
		Vector3 baseposition = transform.position;
		for(;;){
			if(recoverTime < 1){
				recoverTime += regenRate*Time.deltaTime;
			}
			if(recoverTime > 1){
				while (t > 0){
					t -= Time.deltaTime * 2f;
					transform.position = baseposition + new Vector3(Mathf.Sin(Time.time * 100f)-.5f, 0f, 0f) * .1f;
					yield return 0;
				}
				accelMod = 1;
				recoverTime = 1;
				Suicide("UnFreeze");
				speed = 18f;
				resistRate = 0.33f;
				gameObject.GetComponent<Animator>().SetBool("isBurning", true);
			}
			yield return 0;
		}
	}
}
