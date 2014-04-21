using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*public abstract class A_Star : MonoBehaviour {

	private List<GameObject> openList = new List<GameObject>();
	private List<GameObject> closedList = new List<GameObject>();
	private List<GameObject> neighbours = new List<GameObject>();

	public GameObject AStar(GameObject NodeFrom, GameObject NodeGoal){
		openList.Clear();
		closedList.Clear();
		int currentG = 0;
		GameObject temp, currentNode = NodeFrom;
		Node script;
		while(!closedList.Contains(NodeGoal)){
			script = currentNode.GetComponent<Node>();
			parseNeighbours (script);
			foreach(GameObject neighbour in neighbours){
				if(!openList.Contains(neighbour)){
					openList.Add(neighbour);
					neighbour.GetComponent<NodeData>().parent = currentNode;
					neighbour.GetComponent<NodeData>().G = currentG;
					neighbour.GetComponent<NodeData>().F = neighbour.GetComponent<NodeData>().G+getHeuristic(neighbour, NodeGoal);
				}
				else{
					if(currentG < neighbour.GetComponent<NodeData>().G ){
						neighbour.GetComponent<NodeData>().G = currentG;
						neighbour.GetComponent<NodeData>().parent = currentNode;
						neighbour.GetComponent<NodeData>().F = neighbour.GetComponent<NodeData>().G+getHeuristic(neighbour, NodeGoal);
					}
				}
			}
			foreach(GameObject Node in openList){
				if(Node.GetComponent<NodeData>().F<currentNode.GetComponent<NodeData>().F){
					currentNode = Node;
				}
			}
			closedList.Add(currentNode);
		}




	}

	void parseNeighbours(Node input){
		neighbours.Clear();
		neighbours.Add (input.upNeighbour);
		neighbours.Add (input.downNeighbour);
		neighbours.Add (input.leftNeighbour);
		neighbours.Add (input.rightNeighbour);

		foreach(GameObject neighbour in neighbours){
			if(neighbour==null || closedList.Contains(neighbour)){
				neighbours.Remove (neighbour);
			}
		}
	}

	public int getHeuristic(GameObject NodeFrom, GameObject NodeGoal){
		Node from = NodeFrom.GetComponent<Node>();
		Node goal = NodeGoal.GetComponent<Node>();
		int result = 0;
		result += (Mathf.Abs(goal.getX()-from.getX()));
		result += (Mathf.Abs (goal.getY()-from.getY()));
		return result;
	}
}*/
