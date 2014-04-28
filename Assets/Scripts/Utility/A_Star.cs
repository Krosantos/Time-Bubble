using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Star : MonoBehaviour {

	List<GameObject> neighbours = new List<GameObject>();
	List<GameObject> openList = new List<GameObject>();
	List<GameObject> closedList = new List<GameObject>();
	List<GameObject> cameFrom = new List<GameObject>();

	public GameObject[] AStar(GameObject NodeFrom, GameObject NodeGoal){

		neighbours.Clear();
		openList.Clear();
		closedList.Clear();
		cameFrom.Clear();

		GameObject currentNode = NodeFrom;
		NodeFrom.GetComponent<NodeData>().parent = NodeFrom;
		openList.Add (NodeFrom);
		int gScore = 0;
		NodeFrom.GetComponent<NodeData>().F=int.MaxValue;
		NodeFrom.GetComponent<NodeData>().G = 0;

		int j =0;
		while(openList.Count > 0){
			//Select best F from openList
			Debug.Log ("Trial #"+(j+1));
			j++;
			currentNode = openList[0];
			for(int i=0;i<openList.Count;i++){
				if(openList[i].GetComponent<NodeData>().F<currentNode.GetComponent<NodeData>().F){
					currentNode = openList[i];
				}
			}

			Debug.Log("Current Node is: "+currentNode);

			if(currentNode == NodeGoal){
				//Celebrate great victory, make path back.
				Debug.Log ("OpenList Count: "+openList.Count);
				Debug.Log ("Victory!");
				bakePath (NodeGoal, NodeFrom);
				return cameFrom.ToArray();
			}
			openList.Remove (currentNode);
			closedList.Add (currentNode);
			Debug.Log ("Clos: "+printList(closedList));

			//Process Neighbours.
			parseNeighbours(currentNode.GetComponent<Node>());
			Debug.Log ("Neighbours: "+printList(neighbours));
			foreach(GameObject neighbour in neighbours){
				int tempG = gScore+1;
				if(!openList.Contains(neighbour)||tempG<neighbour.GetComponent<NodeData>().G){
					neighbour.GetComponent<NodeData>().parent=currentNode;
					Debug.Log("The parent of "+neighbour+ "is "+neighbour.GetComponent<NodeData>().parent);
					neighbour.GetComponent<NodeData>().G = tempG;
					neighbour.GetComponent<NodeData>().F = neighbour.GetComponent<NodeData>().G+getHeuristic(neighbour,NodeGoal);
					if(!openList.Contains(neighbour))openList.Add(neighbour);
				}
			}
			Debug.Log ("OpenList Count: "+openList.Count);
			Debug.Log ("Open: "+printList (openList));
		}
		Debug.Log ("Failure!");
		return null;


	}

	void bakePath(GameObject goal, GameObject origin){
		GameObject currentNode = goal;
		cameFrom.Clear();
		cameFrom.Add (goal);
		for(int i=0;i<50;i++){
		//while(currentNode.GetComponent<NodeData>().parent != origin){
			cameFrom.Add(currentNode.GetComponent<NodeData>().parent);
			currentNode = currentNode.GetComponent<NodeData>().parent;
			if(currentNode == origin)break;
		}
		Debug.Log ("Return path: "+printList(cameFrom));
	}

	void parseNeighbours(Node input){
		neighbours.Clear();
		List<GameObject> result = input.getNeighbours();
		for(int i=0;i<result.Count;i++){
			if(closedList.Contains(result[i])){
				result.Remove(result[i]);
			}
		}
		neighbours = result;
	}

	string printList(List<GameObject> input){
		string result = "";
		foreach(GameObject item in input){
			result+=(item+ " ");
		}
		return result;
	}

	public int getHeuristic(GameObject NodeFrom, GameObject NodeGoal){
		Node from = NodeFrom.GetComponent<Node>();
		Node goal = NodeGoal.GetComponent<Node>();
		int result = 0;
		result += (Mathf.Abs(goal.getX()-from.getX()));
		result += (Mathf.Abs (goal.getY()-from.getY()));
		return result;
	}
}
