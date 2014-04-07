using UnityEngine;
using System.Collections;

public class FloorMaker{

	public Vector2 position;
	public Vector2 direction;

	float turnchance;
	float turnaroundchance;


	public FloorMaker(Vector2 p, Vector2 d, float tc, float tac){
		position = p;
		direction = d;
		turnchance = tc;
		turnaroundchance = tac;

	}

	public Vector2 Move(){

		float r = Random.Range(0f,1f);
		if (r < turnchance){
			Turn();
		}else if (r < turnchance + turnaroundchance){
			TurnAround();
		}

		position += direction;

		return position;

	}

	public void Turn(){
		float r = Random.Range(0f,1f);
		if (r < .5f){
			TurnLeft();
		}else{
			TurnRight();
		}
	}

	private void TurnLeft(){
		if (direction == Vector2.up)
			direction = -Vector2.right;
		else if (direction == Vector2.right)
			direction = Vector2.up;
		else if (direction == -Vector2.up)
			direction = Vector2.right;
		else
			direction = -Vector2.up;
	}

	private void TurnRight(){
		if (direction == Vector2.up)
			direction = Vector2.right;
		else if (direction == Vector2.right)
			direction = -Vector2.up;
		else if (direction == -Vector2.up)
			direction = -Vector2.right;
		else
			direction = Vector2.up;
	}

	private void TurnAround(){
		direction *= -1f;
	}


}
