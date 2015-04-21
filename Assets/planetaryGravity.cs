using UnityEngine;
using System.Collections;

public class planetaryGravity : MonoBehaviour {

	public float maxGravDist = 2.5f;
	public float maxGravity = 75.0f;
	private bool inGravity = false;
	private GameObject lePlanet;
	
	GameObject[] planets;
	
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("planet");
		foreach(GameObject planet in planets) {
			float dist = Vector3.Distance(planet.transform.position, transform.position);
			if (dist <= maxGravDist) {
				inGravity = true;
				lePlanet = planet;
			}
		}
	}

	void FixedUpdate () {
		if(inGravity == true) {
			float dist = Vector3.Distance(lePlanet.transform.position, transform.position);
			if (dist <= maxGravDist) {
				Vector3 v = lePlanet.transform.position - transform.position;
				GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
			}
			else {
				Color color = GetComponent<Renderer>().material.color;
				color.a -= 0.01f;
				GetComponent<Renderer>().material.color = color;
				if (color.a < 0.1f){
					Destroy(this.gameObject);
				}
			}
		}
		else {
			Color color = GetComponent<Renderer>().material.color;
			color.a -= 0.01f;
			GetComponent<Renderer>().material.color = color;
			if (color.a < 0.1f){
				Destroy(this.gameObject);
			}
		}
	}
}
