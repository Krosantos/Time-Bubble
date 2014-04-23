using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		int r = Random.Range(0,sprites.Length);
		GetComponent<SpriteRenderer>().sprite = sprites[r];
	}

}
