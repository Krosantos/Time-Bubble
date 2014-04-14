using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour {

	Light2D light2d;
	float t;
	public Color neutralcolor, weakattackcolor, strongattackcolor;

	float camsize;
	GameObject cam;
	public float zoomoffset = 5f;
	public float zoomamount = .5f;

	float intensity;

	float targetangle;
	Color attackcolor;
	float targetradius;

	bool beamOn = false;

	// Use this for initialization
	void Start () {
		light2d = GetComponent<Light2D>();
		cam = Camera.main.gameObject;
		camsize = cam.camera.orthographicSize;

		Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}

	void OnDestroy(){
		Light2D.UnregisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.UnregisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.UnregisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 distance = target - transform.position;

		float angle = Mathf.Atan2(distance.y, distance.x);
		angle = angle * Mathf.Rad2Deg;

		light2d.LightConeStart = angle;

		if (Input.GetMouseButtonDown(0)) beamOn = !beamOn;



		if (beamOn){
			if (t < 1f) t += Time.deltaTime * 10f;
			intensity = Mathf.Clamp((distance.magnitude-10f)/2f, 0f, 1f);
			targetangle = Mathf.Lerp(90f, 2f, intensity);
			attackcolor = Color.Lerp(weakattackcolor, strongattackcolor, intensity);
			targetradius = Mathf.Lerp(7.5f, 20f, intensity);
		}else if (!beamOn && t > 0f){
			t -= Time.deltaTime * 7f;
		}

		// lerp light angle based on proximity
		light2d.LightConeAngle = Mathf.Lerp (360f, targetangle, t);
		light2d.LightRadius = Mathf.Lerp (5f, targetradius, t);
		light2d.LightColor = Color.Lerp(neutralcolor, attackcolor, t);
		Camera.main.orthographicSize = Mathf.Lerp (camsize, camsize - zoomamount, t);
		cam.GetComponent<CameraFollow>().offset = Vector3.Lerp(Vector3.zero, distance.normalized * zoomoffset, t);
	}

	void OnLightEnter(Light2D l, GameObject g){
		Debug.Log(g.name);

	}

	void OnLightStay(Light2D l, GameObject g){
		if (g.tag == "Enemy" && beamOn){
			float range = Mathf.Clamp(1f - ((g.transform.position - transform.position).magnitude / l.LightRadius), 0f, 1f);
			float compositeintensity = range * intensity;
			g.GetComponent<MobBase>().Petrify(compositeintensity);
		}
	}

	void OnLightExit(Light2D l, GameObject g){
		if (g == null) return;
		if (g.tag == "Enemy"){
			g.GetComponent<MobBase>().Recover();
		}
	}
}
