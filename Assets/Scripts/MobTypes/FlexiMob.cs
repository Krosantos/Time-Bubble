using UnityEngine;
using System.Collections;

public class FlexiMob : MobBase {

	public float Speed = 1f;
	public float DetectRange = 1f;
	public float TargetRange = 1f;
	public float EngageRange = 1f;
	public float ResistRate = 1f;
	public float RegenRate = 1f;
	public float Acceleration = 1f;

	public FlexiMob (){
		speed = Speed;
		health = 2f;
		detectRange = DetectRange;
		targetRange = TargetRange;
		engageRange = EngageRange;
		resistRate = ResistRate;
		regenRate = RegenRate;
		accel = Acceleration;
	}
}
