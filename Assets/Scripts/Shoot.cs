using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject projectile;
	public float shootspeed = 100f;
	public float shootoffset = 2f;
	public bool automatic = false;
	public float rate = .5f;

	private float timer = 0f;
	
	void Update () {

		if (Input.GetMouseButtonDown(0)){
			ShootAtMouse();
			timer = 0f;
		}

		if (Input.GetMouseButton(0) && automatic){
			timer += Time.deltaTime;
			if (timer > rate){
				ShootAtMouse();
				timer = 0f;
			}
		}

	}

	void ShootAtMouse(){
		ShootAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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

		newprojectile.rigidbody.velocity = Time.deltaTime * 50f * ((direction + .025f * (Vector3)Random.insideUnitCircle).normalized * shootspeed);

	}

}
