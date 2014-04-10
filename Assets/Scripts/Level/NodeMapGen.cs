using UnityEngine;
using System.Collections;

public class NodeMapGen : MonoBehaviour {

	public static int xOffSet;
	public static int yOffSet;
	public static GameObject [,] nodeMap;
	void Start () {
		//Set up NodeMap
		xOffSet = 1-(Node.minX);
		yOffSet = 1-(Node.minY);
		nodeMap = new GameObject[(Node.maxX+xOffSet+2),(Node.maxY+yOffSet+2)]; //+2 is to create a buffer edge
		//Populate empty map
		for(int x=0;x<(nodeMap.GetLength(0));x++){
			for(int y=0;y<(nodeMap.GetLength(1));y++){
				nodeMap[x,y] = null;
			}
		}

		//Stick nodes in the map
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Node");
		for(int i=0;i<temp.Length;i++){
			Node node = temp[i].GetComponent<Node>();
			node.refactorX(xOffSet);
			node.refactorY(yOffSet);
			nodeMap[node.getX(),node.getY()]=temp[i];
		}

		//Now that they're in the big map, tell them who
		//the neighbours are.
		for(int i=0;i<temp.Length;i++){
			Node node = temp[i].GetComponent<Node>();
			node.mapNeighbours();
		}
		//PrintDimensions();
		//PrintMap();
	}

	//I used this while debugging.
	void PrintDimensions(){
		Debug.Log ("X: "+(Node.maxX+xOffSet+2));
		Debug.Log ("Y: "+(Node.maxY+yOffSet+2));
		Debug.Log(nodeMap.GetLength(0));
		Debug.Log(nodeMap.GetLength(1));
	}

	//Also helpful for debugging.
	void PrintMap(){
		string map = "";
		for(int x=0;x<(nodeMap.GetLength(0));x++){
			for(int y=0;y<(nodeMap.GetLength(1));y++){
				if(nodeMap[x,y]==null){
					map += "X";
				}
				else{
					map += "O";
				}
			}
			Debug.Log(map);
			map = "";
		}
	}
}
