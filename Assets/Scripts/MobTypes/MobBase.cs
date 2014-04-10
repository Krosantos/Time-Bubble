using UnityEngine;
using System.Collections;

public class MobBase : MonoBehaviour {
	
	static private GameObject player;
	private float _health;
	private float _detectRange; //Range at which mob loses you
	private float _targetRange; //Range at which mob aggresses
	private float _engageRange; //Range mob tries to fight at
	private AIStates AIState;
	private bool isFighting;

	public GameObject lastNode;
	public GameObject nextNode;
	private float nodeDist;
	private CharacterController self;
	private Vector3 velocity;
	private Vector3 target;
	private float _speed;
	private float _accel;
	private float xmove;
	private float ymove;
	private float accelMod=1;
	
	
	#region Get/Set Variables
	
	public float speed{
		get {return _speed;}
		set {_speed = value;}
	}
	
	public float accel{
		get {return _accel;}
		set {_accel = value;}
	}
	
	public float targetRange{
		get {return _targetRange;}
		set {_targetRange = value;}
	}
	
	public float health{
		get {return _health;}
		set {_health = value;}
	}
	
	public float detectRange{
		get {return _detectRange;}
		set {_detectRange = value;}
	}
	
	public float engageRange{
		get {return _engageRange;}
		set {_engageRange = value;}
	}
	#endregion
	#region Start/Update
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		lockNode();
		self = GetComponent<CharacterController>();
		StartCoroutine ("updateState");
	}
	
	void Update(){
		
		//Check for death
		if(health <= 0){
			Destroy (gameObject);
		}
		
		//Reset move variables
		velocity = self.velocity;
		xmove = 0f;
		ymove = 0f;
		
		
		//Determine movetarget
		switch (AIState){
		case AIStates.Idle:
			accelMod = .02f;
			//lockNode();
			Idle ();
			break;
		case AIStates.Pursue:
			accelMod = 1;
			Pursue();
			break;
		case AIStates.Attack:
			accelMod = 1;
			Pursue();
			break;
		}
		
		//Move to movetarget
		velocity.x = Mathf.Lerp(velocity.x, xmove * speed, Time.deltaTime * accel*accelMod);
		velocity.y = Mathf.Lerp(velocity.y, ymove * speed, Time.deltaTime * accel*accelMod);
		//Debug.Log (xmove + ", " + ymove);
		self.Move(velocity*Time.deltaTime);
	}
	#endregion
	#region State Directions
	protected virtual void Idle(){
		target = (nextNode.transform.position - transform.position);
		nodeDist=target.magnitude;
		target.Normalize();
		xmove = target.x;
		ymove = target.y;
		if(nodeDist<0.03f){
			lastNode=nextNode;
			nextNode=lastNode.GetComponent<Node>().getNextNode(lastNode);
		}
	}
	
	protected virtual void Pursue(){

		target = (player.transform.position - transform.position);
		target.Normalize();
		xmove = target.x;
		ymove = target.y;
	}
	
	protected void Attack(){

	}
	

	#endregion
	#region State Calculation
	AIStates calcState(){
		//Determines an AI's current state
		//TODO:Contemplate Fleeing
		
		float distance = Calc.getRangeFrom(gameObject, player);
		
		if(distance > detectRange){
			isFighting = false;
			//Debug.Log ("IDLE");
			return AIStates.Idle;
		}
		else if(distance <= targetRange && distance > engageRange){
			isFighting = true;
			//Debug.Log ("CHASE");
			return AIStates.Pursue;
		}
		else if(distance < engageRange){
			isFighting = true;
			//Debug.Log("ATTACK");
			return AIStates.Attack;
		}
		else if(isFighting && distance < detectRange){
			isFighting = true;
			//Debug.Log ("CHASE");
			return AIStates.Pursue;
		}
		else{
			isFighting = false;
			//Debug.Log("IDLE BUT BAD IDLE");
			return AIStates.Idle;
		}
	}
	
	public bool getIsFighting(){
		return isFighting;
	}
	
	IEnumerator updateState(){
		for(;;){
			AIState = calcState ();
			yield return new WaitForSeconds(.5f);
		}
	}
	#endregion
	public void lockNode(){
		if(NodeMapGen.nodeMap[(int)transform.position.x+NodeMapGen.xOffSet,(int)transform.position.y+NodeMapGen.yOffSet]!=null){
			nextNode=NodeMapGen.nodeMap[(int)transform.position.x+NodeMapGen.xOffSet,(int)transform.position.y+NodeMapGen.yOffSet];
		}
	}
}
