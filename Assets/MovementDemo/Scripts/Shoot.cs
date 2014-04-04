using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject projectile;
	public float shootspeed = 100f;
	public float shootoffset = 2f;
	public float bulletlifetime = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//RaycastHit hit = new RaycastHit();
		//if(Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit)){
			// use hit.point here to make something look at mouse (gun?)
			if (Input.GetMouseButtonDown(0)){
				ShootAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
//		}

	}

	void ShootAt(Vector3 target){
		target = new Vector3(target.x, target.y, 0f);
		Vector3 distance = target - transform.position;
		Vector3 direction = distance.normalized;
		GameObject newprojectile = 
			Instantiate(projectile, 
			            transform.position + direction * shootoffset + (Vector3)Random.insideUnitCircle * .25f, 
			            Quaternion.identity) as GameObject;

		float angle = Mathf.Atan2(distance.y, distance.x);
		angle = angle * Mathf.Rad2Deg;

		newprojectile.transform.rotation = Quaternion.Euler(0f,0f,angle-90f);

		newprojectile.rigidbody2D.velocity = ((direction + .025f * (Vector3)Random.insideUnitCircle).normalized * shootspeed);

		Destroy(newprojectile, bulletlifetime);
	}
}
