using UnityEngine;
using System.Collections;

public class TestMob : MobBase {

	public TestMob (){
		speed = 4f;
		health = 2f;
		detectRange = 5f;
		targetRange = 5f;
		engageRange = 5f;
		accel = 10f;
	}
}
