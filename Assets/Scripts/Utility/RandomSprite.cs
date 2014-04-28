using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour {

	public Texture[] sprites;


	// Use this for initialization
	void Start () {
		int r = Random.Range(0,sprites.Length);
		renderer.material.mainTexture = sprites[r];
	}

}
