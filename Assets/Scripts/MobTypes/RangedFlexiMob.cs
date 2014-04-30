using UnityEngine;
using System.Collections;

public class RangedFlexiMob : MobBase {

	public GameObject projectile;
	protected bool isShooting;
	public float Speed = 1f;
	public float DetectRange = 1f;
	public float TargetRange = 1f;
	public float EngageRange = 1f;
	public float ResistRate = 1f;
	public float RegenRate = 1f;
	public float Acceleration = 1f;
	
	public override void Start (){
		base.Start();
		StartCoroutine ("shoot");
		speed = Speed;
		health = 2f;
		detectRange = DetectRange;
		targetRange = TargetRange;
		engageRange = EngageRange;
		resistRate = ResistRate;
		regenRate = RegenRate;
		accel = Acceleration;
	}

	public override void Update ()
		{
			if (accelMod <= 0f){
				if (!isPetrified) AudioManager.instance.Play(AudioManager.instance.petrifysound);
				isPetrified = true;
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
				Attack();
				break;
			}
			Move ();
		}

	protected override void Idle ()
	{
		isShooting = false;
		base.Idle ();
	}

	protected override void Pursue ()
	{
		isShooting = false;
		base.Pursue ();
	}

	protected override void Attack ()
	{	
		target = transform.position;
		isShooting = true;
	}

	protected virtual IEnumerator shoot(){
		GameObject temp;
		for(;;){
			if(isShooting){
				temp = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
				temp.GetComponent<SlowOrb>().direction = (player.transform.position-transform.position);
			}
			yield return new WaitForSeconds(2.5f);
		}
	}
}
