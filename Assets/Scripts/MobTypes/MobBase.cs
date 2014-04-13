using UnityEngine;
using System.Collections;

public class MobBase : MonoBehaviour {
	
	static private GameObject player;
	public LayerMask collideMask;
	private float _health;
	private float _detectRange; //Range at which mob loses you
	private float _targetRange; //Range at which mob aggresses
	private float _engageRange; //Range mob tries to fight at
	private AIStates AIState;
	private bool isFighting;

	public GameObject lastNode;
	public GameObject nextNode;
	private float nodeDist;
	private Rigidbody self;
	private Vector3 moveVec;
	private Vector3 target;
	private float _speed;
	private float _accel;
	private float _regenRate;
	private float _resistRate; //Unintuitively, 1 means no resist, 0 means immune.
	private float accelMod=1;
	
	
	#region Get/Set Variables
	
	public float speed{
		get {return _speed;}
		set {_speed = value;}
	}

	public float regenRate{
		get {return _regenRate;}
		set {_regenRate = value;}
	}

	public float resistRate{
		get {return _resistRate;}
		set {_resistRate = value;}
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
		self = GetComponent<Rigidbody>();
		StartCoroutine ("updateState");
	}
	
	void Update(){
		
		//Check for death
		if(health <= 0){
			Destroy (gameObject);
		}
		
		//Determine movetarget
		switch (AIState){
		case AIStates.Idle:
			Idle ();
			break;
		case AIStates.Pursue:
			Pursue();
			break;
		case AIStates.Attack:
			Pursue();
			break;
		}
		Move ();
	}
	#endregion
	#region State Directions
	protected virtual void Idle(){
		target = (nextNode.transform.position - transform.position);
		nodeDist=target.magnitude;
		if(nodeDist<0.2f){
			lastNode=nextNode;
			nextNode=lastNode.GetComponent<Node>().getNextNode(lastNode);
		}
	}
	
	protected virtual void Pursue(){

		target = (player.transform.position-transform.position);
	}
	
	protected void Attack(){

	}

	void Move(){

		moveVec = target.normalized*speed*Time.deltaTime*accelMod;
		float moveDist = moveVec.magnitude;
		if(!Physics.Raycast(transform.position,target,moveDist+.5f,collideMask)){
			self.MovePosition (moveVec+transform.position);
		}
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
		else if( distance <= targetRange && distance > engageRange){
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

	#region Light Effects
	public void Petrify(float compositeIntensity){
		StopCoroutine("UnFreeze");
		if(accelMod > 0f){
			accelMod-=compositeIntensity*Time.deltaTime;
		}
		if(accelMod < 0f){
			accelMod = 0f;
		}
	}
	public void Recover(){
		StartCoroutine("UnFreeze");
	}

	IEnumerator UnFreeze(){
		for(;;){
			if(accelMod < 1){
				accelMod += regenRate*Time.deltaTime;
			}
			if(accelMod > 1){
				accelMod = 1;
				Suicide("UnFreeze");
			}
			yield return 0;
		}
	}

	void Suicide(string targetCR){
		StopCoroutine("targetCR");
	}

	#endregion


	#endregion
	public void lockNode(){
		if(NodeMapGen.nodeMap[(int)transform.position.x+NodeMapGen.xOffSet,(int)transform.position.y+NodeMapGen.yOffSet]!=null){
			nextNode=NodeMapGen.nodeMap[(int)transform.position.x+NodeMapGen.xOffSet,(int)transform.position.y+NodeMapGen.yOffSet];
		}
	}
}
