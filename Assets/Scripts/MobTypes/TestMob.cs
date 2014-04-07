using UnityEngine;
using System.Collections;

public class TestMob : MobBase {

	public TestMob (){
		speed = 2f;
		health = 2f;
		detectRange = 50f;
		targetRange = 20f;
		engageRange = 5f;
		accel = .0f;
	}
}
