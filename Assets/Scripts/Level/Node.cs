using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private int xCor;
	private int yCor;
	public bool isOn = true;

	//Just while I test
	public Material nodeOff;
	public Material nodeOn;

	public static int minX = int.MaxValue;
	public static int minY = int.MaxValue;
	public static int maxX = int.MinValue;
	public static int maxY = int.MinValue;
	public static int tileCount = 0;
	private List<GameObject> neighbours = new List<GameObject>();

	public int neighbourCount;
	public GameObject upNeighbour;
	public GameObject downNeighbour;
	public GameObject leftNeighbour;
	public GameObject rightNeighbour;

	void Awake() {
		tileCount ++;
		xCor = (int)transform.position.x;
		yCor = (int)transform.position.y;
		if(xCor>maxX){
			maxX = xCor;
		}
		if(xCor<minX){
			minX = xCor;
		}
		if(yCor>maxY){
			maxY = yCor;
		}
		if(yCor<minY){
			minY = yCor;
		}
	}

	void Update(){
		if(isOn){
			transform.renderer.material = nodeOn;
		}
		else transform.renderer.material = nodeOff;
	}

	public int getX(){
		return xCor;
	}
	public int getY(){
		return yCor;
	}

	public void refactorX(int offset){
		xCor += offset;
		//Debug.Log ("Now, it's "+xCor);
	}
	public void refactorY(int offset){
		yCor += offset;
		//Debug.Log ("Now, it's "+yCor);
	}

	public void turnOn(){
		isOn = true;
	}

	public void turnOff(){
		isOn = false;
	}

	public bool getOn(){
		return isOn;
	}

	public GameObject getNextNode(GameObject lastNode){
		int randChoice;
		GameObject result = lastNode;
		randChoice = (int)Random.Range(0,neighbourCount);
			result = neighbours[randChoice];
		return result;
	}

	public List<GameObject> getNeighbours(){
		return neighbours;
	}

	/*public GameObject getNextNode(GameObject lastNode){
		bool check = false;
		int randChoice;
		GameObject result = lastNode;
		if(neighbourCount == 1){
			return result;
		}
		else{
			while(!check){
				randChoice = Random.Range(1,5);
				switch(randChoice){
				case 1:
					if(upNeighbour != null && upNeighbour != lastNode){
						//if(upNeighbour.GetComponent<Node>().isOn){
						check = true;
						result = upNeighbour;
						//}
					} 
					break;
				case 2:
					if(downNeighbour != null && downNeighbour != lastNode){
						//if(downNeighbour.GetComponent<Node>().isOn){
						check = true;
						result = downNeighbour;
						//}
					}
					break;
				case 3:
					if(leftNeighbour != null && leftNeighbour != lastNode){
						//if(leftNeighbour.GetComponent<Node>().isOn){
						check = true;
						result = leftNeighbour;
						//}
					}
					break;
				case 4:
					if(rightNeighbour != null && rightNeighbour != lastNode){
						//if(rightNeighbour.GetComponent<Node>().isOn){
						check = true;
						result = rightNeighbour;
						//}
					}
					break;
				default:
					break;
				}
			}
		}
		return result;
	}*/

	public void mapNeighbours(){
		upNeighbour = NodeMapGen.nodeMap[xCor,yCor+1];
		downNeighbour = NodeMapGen.nodeMap[xCor,yCor-1];
		leftNeighbour = NodeMapGen.nodeMap[xCor-1,yCor];
		rightNeighbour = NodeMapGen.nodeMap[xCor+1,yCor];
		if(upNeighbour!=null)neighbours.Add (upNeighbour);
		if(downNeighbour!=null)neighbours.Add (downNeighbour);
		if(leftNeighbour!=null)neighbours.Add (leftNeighbour);
		if(rightNeighbour!=null)neighbours.Add (rightNeighbour);
		neighbourCount = neighbours.Count;
	}
}
