﻿using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	void OnCollisionEnter2D(){
		Destroy(gameObject);
	}
}
