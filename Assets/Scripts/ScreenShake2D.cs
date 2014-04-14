using UnityEngine;
using System.Collections;

/*
 * This script MUST be used with a camera that is a child of an empty game object.
 * 
 * As a static class, ScreenShake2D does not need to be placed on an object as a component.  
 * However, one monobehavior's Update() method must call ScreenShake2D.Update() once a frame for shake to update properly.
 * 
 * Camera should be set to (0,0,0) within its parent.  Place any scripts for camera following on the parent.
 * 
 * Intensity refers to amount of shake. Decay is the amount intensity is decreased each second for the duration of the shake.
 * 
 * THINGS TO ADD SOMEDAY MAYBE: 
 * 		~ stop on ___ intensity
 * 		~ reverse shake (kinda works right now with negative decay values)
 * 		~ take a Vector2 as an argument (not strictly world X/Y)
 */

public static class ScreenShake2D{

	static float shake_intensityX, shake_decayX, shake_intensityY, shake_decayY;
	static Transform maincam;
	static float xseed, yseed;

	public static void Update (){
		
		maincam = Camera.main.transform;
		maincam.localPosition = Vector3.zero;

		if (shake_intensityX > 0){
			//Vector3 randX = Random.Range(-1f,1f) * maincam.right * shake_intensityX; // RANDOM
			Vector3 randX = (Mathf.PerlinNoise(Time.time*30f, xseed)*2f-1f) * maincam.right * shake_intensityX; // PERLIN NOISE
			maincam.localPosition += randX;
			shake_intensityX -= shake_decayX * Time.deltaTime;
		}

		if (shake_intensityY > 0){
			//Vector3 randY = Random.Range(-1f,1f) * maincam.up * shake_intensityY; // RANDOM
			Vector3 randY = (Mathf.PerlinNoise(Time.time*30f, yseed)*2f-1f) * maincam.up * shake_intensityY; // PERLIN NOISE
			maincam.localPosition += randY;
			shake_intensityY -= shake_decayY * Time.deltaTime;
		}
	}

	public static void SetShake(float intensity){
		shake_intensityX = intensity;
		shake_intensityY = intensity;
	}

	public static void SetDecay(float decay){
		shake_decayX = decay;
		shake_decayY = decay;
	}
	
	public static void Shake(float intensity, float decay){
		ShakeX(intensity, decay);
		ShakeY(intensity, decay);
	}
	
	public static void ShakeX(float intensity, float decay){
		xseed = Random.Range(0f,10f);
		shake_intensityX = intensity;
		shake_decayX = decay;
	}

	public static void ShakeY(float intensity, float decay){
		yseed = Random.Range(0f,10f);
		shake_intensityY = intensity;
		shake_decayY = decay;
	}

}