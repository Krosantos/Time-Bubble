using UnityEngine;
using System.Collections;

public class TestMob : MobBase {

	public TestMob (){
		speed = 4f;
		health = 2f;
		detectRange = 50f;
		targetRange = 20f;
		engageRange = 10f;
		accel = 10f;
	}

	protected override void Idle ()
	{
		//Debug.Log ("OVERRIDE!");
		base.Idle();
	}
}
