using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapMaker : MonoBehaviour {

	public GameObject floor;
	public GameObject topwall, forwardwall;
	public GameObject start;
	public GameObject door;
	public GameObject player;
	public GameObject key;
	public GameObject[] enemyTypes;
	public static List<GameObject> enemy;

	public float turnchance = .1f;
	public float turnaroundchance = .05f;

	public float splitchance = .005f;
	public float roomchance = .01f;
	public int roomsizeX = 2;
	public int roomsizeY = 2;

	public int maxFloorMakers = 20;

	public int iterations = 112;

	public float minEnemyDistance = 15f;
	public static float enemySpawnRate;  // likelihood of enemy spawning at any given floor tile

	public List<FloorMaker> floorMakers = new List<FloorMaker>();

	Tile[,] tilemap = new Tile[1000,1000];

	Vector2 spawnpoint;
	Vector2 exitpoint;

	// Use this for initialization
	void Awake () {

		ScoreManager.currentlevel++;

		if (ScoreManager.currentlevel == 1){
			enemy = new List<GameObject>(10);
			enemySpawnRate = .05f;
			enemy.Add(enemyTypes[0]);
			enemy.Add(enemyTypes[0]);
			enemy.Add(enemyTypes[0]);
			enemy.Add(enemyTypes[0]);
			enemy.Add(enemyTypes[1]);
		}
		if (ScoreManager.currentlevel > 1) enemy.Add(enemyTypes[1]);
		if (ScoreManager.currentlevel > 1) enemy.Add(enemyTypes[2]);
		if (ScoreManager.currentlevel > 1) enemy.Add(enemyTypes[3]);
		if (ScoreManager.currentlevel > 1) enemy.Add(enemyTypes[4]);
		if (ScoreManager.currentlevel > 1) enemy.Add(enemyTypes[5]);

		enemySpawnRate += .01f;

		InitTileMap();

		MakeFloors();

		PlacePlayer();

	}

	void InitTileMap(){
		for (int i = 0; i < 1000; i++){
			for (int j = 0; j < 1000; j++){
				tilemap[i,j] = new Tile();
			}
		}
	}

	void MakeFloors(){

		floorMakers.Add(new FloorMaker(Vector2.one*500f, Vector2.right, turnchance, turnaroundchance));

		// make start tile
		GameObject startTile = (GameObject)Instantiate(start,Vector2.one*500f,Quaternion.identity);
		startTile.transform.parent = transform;

		// mark start tile in array and check adjacent tiles
		tilemap[(int)startTile.transform.position.x,(int)startTile.transform.position.y].SetTile(startTile);
		tilemap[(int)startTile.transform.position.x,(int)startTile.transform.position.y].isStart = true;
		MarkAdjacentTiles(startTile);
		spawnpoint = startTile.transform.position;

		while (iterations >= 0){
			List<FloorMaker> floorMakersToAdd = new List<FloorMaker>();
			foreach (FloorMaker f in floorMakers){
				Vector2 v = f.Move();
				if (tilemap[(int)v.x,(int)v.y].isEmpty){
					// make a new tile
					GameObject newTile = (GameObject)Instantiate(floor, v, Quaternion.identity);
					newTile.transform.parent = transform;
					// mark tile in array and check adjacent tiles
					tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].SetTile(newTile);
					MarkAdjacentTiles(newTile);
					iterations--;
				}

				// spawn new floormakers to create branches?
				if (Random.Range(0f,1f) < splitchance){
					FloorMaker newf = new FloorMaker(f.position, f.direction, turnchance, turnaroundchance);
					newf.Turn();
					floorMakersToAdd.Add(newf);
				} 

				// make a room?
				if (Random.Range(0f,1f) < roomchance){
					for (int i = 0; i < roomsizeX; i++){
						for (int j = 0; j < roomsizeY; j++){
							if (tilemap[(int)v.x+i,(int)v.y+j].isEmpty){
								GameObject newTile = (GameObject)Instantiate(floor, v + new Vector2(i,j), Quaternion.identity);
								newTile.transform.parent = transform;
								tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].SetTile(newTile);
								MarkAdjacentTiles(newTile);
							}
						}
					}
				} 
			}

			// add any new floormakers
			foreach (FloorMaker f in floorMakersToAdd){
				floorMakers.Add(f);
			}

			// destroy excess floormakers
			while (floorMakers.Count > maxFloorMakers){
				floorMakers.RemoveAt(0);
			}
			
		}

		// find farthest active floormaker at end of generation
		float farthestDoorDistance = 0f;
		Vector2 farthestDoorPosition = new Vector2(); 
		foreach (FloorMaker f in floorMakers){
			float distance = ((Vector2)f.position - (Vector2)startTile.transform.position).magnitude;
			if (distance > farthestDoorDistance){
				farthestDoorDistance = ((Vector2)f.position - (Vector2)startTile.transform.position).magnitude;
				farthestDoorPosition = f.position;
			}
		}

		// make a door at farthest floormaker location
		Tile t = tilemap[(int)farthestDoorPosition.x,(int)farthestDoorPosition.y];
		Destroy(t.tile);
		GameObject newDoor = (GameObject)Instantiate(door, farthestDoorPosition, Quaternion.identity);
		newDoor.transform.parent = transform;
		tilemap[(int)newDoor.transform.position.x,(int)newDoor.transform.position.y].SetTile(newDoor);
		tilemap[(int)newDoor.transform.position.x,(int)newDoor.transform.position.y].isDoor = true;

		// find key location & place enemies
		float distanceFromSpawn = 10f; // these two variables also function as the minimum key distance from either location
		float distanceFromDoor = 10f;
		Vector2 keyPosition = new Vector2(); 
		for (int i = 0; i < 1000; i++){
			for (int j = 0; j < 1000; j++){
				if (!tilemap[i,j].isEmpty && !tilemap[i,j].isStart && !tilemap[i,j].isDoor){
					Vector2 tileposition = tilemap[i,j].tile.transform.position;
					float tileDistanceFromSpawn = ((Vector2)tileposition - (Vector2)startTile.transform.position).magnitude;
					float tileDistanceFromDoor = ((Vector2)tileposition - (Vector2)newDoor.transform.position).magnitude;

					if ((tileDistanceFromSpawn > distanceFromSpawn) && (tileDistanceFromDoor > distanceFromDoor)){
						distanceFromSpawn = tileDistanceFromSpawn;
						distanceFromDoor = tileDistanceFromDoor;
						keyPosition = tileposition;
					}

					if (tileDistanceFromSpawn > minEnemyDistance){
						float r = Random.Range(0f,1f);
						if (r < enemySpawnRate){
							int randint = Random.Range(0, enemy.Count);
							Instantiate(enemy[randint], tileposition, Quaternion.identity);
						}
					}
				}
			}
		}

		// make a key
		t = tilemap[(int)keyPosition.x,(int)keyPosition.y];
		Destroy(t.tile);
		GameObject newKey = (GameObject)Instantiate(key, keyPosition, Quaternion.identity);
		newKey.transform.parent = transform;
		tilemap[(int)newKey.transform.position.x,(int)newKey.transform.position.y].SetTile(newKey);

		
		MakeWalls();
	}

	void MakeWalls(){
		for (int i = 0; i < 1000; i++){
			for (int j = 0; j < 1000; j++){
				if (tilemap[i,j].mightBeWall){
					if (!tilemap[i,j-1].isEmpty && !tilemap[i,j-1].mightBeWall){
						GameObject newTile = (GameObject)Instantiate(forwardwall, new Vector2(i,j), Quaternion.identity);
						newTile.transform.parent = transform;
						tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].SetTile(newTile);
						tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].mightBeWall = true;
					}else{
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i,j), Quaternion.identity);
						newTile.transform.parent = transform;
						tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].SetTile(newTile);
						tilemap[(int)newTile.transform.position.x,(int)newTile.transform.position.y].mightBeWall = true;
					}




				}
			}
		}

		for (int i = 0; i < 1000; i++){
			for (int j = 0; j < 1000; j++){
				if (tilemap[i,j].mightBeWall){

					// if any adjacent tiles are empty, make another wall. helps with padding
					Tile up = tilemap[i,j+1];
					Tile down = tilemap[i,j-1];
					Tile left = tilemap[i-1,j];
					Tile right = tilemap[i+1,j];
					Tile upleft = tilemap[i-1,j+1];
					Tile upright = tilemap[i+1,j+1];
					Tile downleft = tilemap[i-1,j-1];
					Tile downright = tilemap[i+1,j-1];
					if (up.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i,j+1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (down.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i,j-1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (left.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i-1,j), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (right.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i+1,j), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (upleft.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i-1,j+1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (upright.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i+1,j+1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (downleft.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i-1,j-1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
					if (downright.isEmpty){
						GameObject newTile = (GameObject)Instantiate(topwall, new Vector2(i+1,j-1), Quaternion.identity);
						newTile.transform.parent = transform;
					}
				}
			}
		}
	}

	void MarkAdjacentTiles(GameObject tile){

		Tile up = tilemap[(int)tile.transform.position.x,(int)tile.transform.position.y+1];
		Tile down = tilemap[(int)tile.transform.position.x,(int)tile.transform.position.y-1];
		Tile left = tilemap[(int)tile.transform.position.x-1,(int)tile.transform.position.y];
		Tile right = tilemap[(int)tile.transform.position.x+1,(int)tile.transform.position.y];
		Tile upleft = tilemap[(int)tile.transform.position.x-1,(int)tile.transform.position.y+1];
		Tile upright = tilemap[(int)tile.transform.position.x+1,(int)tile.transform.position.y+1];
		Tile downleft = tilemap[(int)tile.transform.position.x-1,(int)tile.transform.position.y-1];
		Tile downright = tilemap[(int)tile.transform.position.x+1,(int)tile.transform.position.y-1];
		if (up.isEmpty) up.mightBeWall = true;

		if (down.isEmpty) down.mightBeWall = true;
		if (left.isEmpty) left.mightBeWall = true;
		if (right.isEmpty) right.mightBeWall = true;
		if (upleft.isEmpty) upleft.mightBeWall = true;
		if (upright.isEmpty) upright.mightBeWall = true;
		if (downleft.isEmpty) downleft.mightBeWall = true;
		if (downright.isEmpty) downright.mightBeWall = true;

	}

	void PlacePlayer(){
		if (player != null){
			Instantiate(player, spawnpoint, Quaternion.identity);
		}
	}
}
