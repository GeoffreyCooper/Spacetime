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
		//Physics2D.IgnoreLayerCollision(8,9, true);
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


	//OLD -- can delete
	/*
	void FixedUpdate () {
		foreach(GameObject planet in planets) {
			float dist = Vector3.Distance(planet.transform.position, transform.position);
			if (dist <= maxGravDist) {
				Vector3 v = planet.transform.position - transform.position;
				rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
				inGravity = true;
			}
			//once a being leaves gravity, inGravity is still true
		}
		//destroy if inGravity is false
		if (inGravity == false) {
			Color color = renderer.material.color;
			color.a -= 0.01f;
			renderer.material.color = color;
			if (color.a < 0.1f){
				Destroy(this.gameObject);
			}
		}
	}*/
}
