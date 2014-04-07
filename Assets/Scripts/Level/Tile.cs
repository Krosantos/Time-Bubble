using UnityEngine;
using System.Collections;

public class Tile{

	public bool isEmpty, mightBeWall, isStart;
	public GameObject tile;

	public Tile(){
		isEmpty = true;
		mightBeWall = false;
		tile = null;
	}

	public void SetTile(GameObject t){
		isEmpty = false;
		mightBeWall = false;
		tile = t;
	}

}
