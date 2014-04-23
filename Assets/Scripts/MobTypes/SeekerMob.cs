using UnityEngine;
using System.Collections;

public class SeekerMob : MobBase {

	private GameObject[] path;
	private int tracker = 0;
	private float nextDist;
	public float Speed = 1f;
	public float DetectRange = 1f;
	public float TargetRange = 1f;
	public float EngageRange = 1f;
	public float ResistRate = 1f;
	public float RegenRate = 1f;
	public float Acceleration = 1f;


	public override void Start(){
		base.Start();
		StartCoroutine("updatePathing");
		speed = Speed;
		health = 2f;
		detectRange = DetectRange;
		targetRange = TargetRange;
		engageRange = EngageRange;
		resistRate = ResistRate;
		regenRate = RegenRate;
		accel = Acceleration;
	}

	protected override void Pursue(){
		target = (path[tracker].transform.position-transform.position);
		nextDist = target.magnitude;
		if(nextDist <0.2f && tracker > 0){
			tracker--;
		}
	}

	IEnumerator updatePathing(){
		//for(;;){
			path=gameObject.GetComponent<A_Star>().AStar(lockNode(),NodeMapGen.trackPlayer());
			if(path!=null)tracker = path.Length-1;
			yield return new WaitForSeconds(1f);
		//}
	}
}
