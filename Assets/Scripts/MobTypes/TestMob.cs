using UnityEngine;
using System.Collections;

public class TestMob : MobBase {

	public TestMob (){
		speed = 4f;
		health = 2f;
		detectRange = 20f;
		targetRange = 19f;
		engageRange = 5f;
		resistRate = 1f;
		regenRate = 0.1f;
		accel = 10f;
	}
}
