using UnityEngine;
using System.Collections;

public class orbitAndLaunch : MonoBehaviour {
	
	public float maxGravDist = 2.5f;
	public float minOrbitDis = 3.0f;
	public float maxOrbitDis = 4.0f;
	public float maxGravity = 30.0f;
	public SpringJoint2D theSpringJoint;
	public AudioSource awakeSound;
	public AudioSource launchSound;
	public AudioSource blackHoleSound;
	GameObject[] planets;
	private GameObject thisPlanet;
	private bool inSpace = false;
	private bool timeToDie = false;
	private bool soundHasPlayed = false;
	
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("planet");
		theSpringJoint =  this.gameObject.GetComponent<SpringJoint2D>();
		awakeSound.Play();
	}

	void OnMouseDown() {
		theSpringJoint.enabled = false;
		float distance = Vector3.Distance(thisPlanet.transform.position, transform.position);
		Vector3 u = thisPlanet.transform.position - transform.position;
		launchSound.Play();
		GetComponent<Rigidbody2D>().AddForce(u.normalized * -300);
		inSpace = true;
		GameObject theText = GameObject.Find("Text");
		textPrompts prompts = theText.GetComponent<textPrompts>();
		if (prompts.showMessage == 4) {
			prompts.showMessage = 5;
		}
	}

	void FixedUpdate () {
		if (inSpace == false) {
			foreach(GameObject planet in planets) {
				float dist = Vector3.Distance(planet.transform.position, transform.position);
				if (dist <= maxGravDist) {
					Vector3 v = planet.transform.position - transform.position;
					GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist / maxGravDist) * -maxGravity);
					StartCoroutine(springJ(planet));
				}/*
				else if (dist > maxGravDist) {
					//rigidbody2D.velocity /= 10;
					theSpringJoint.connectedBody = planet.rigidbody2D;
					theSpringJoint.enabled = true;
				}*/
			}
		}
		else if (inSpace == true) {
			StartCoroutine(waitNow());
			if (timeToDie == true) {
				Color color = GetComponent<Renderer>().material.color;
				color.a -= 0.005f;
				GetComponent<Renderer>().material.color = color;
				if (color.a < 0.1f){
					Destroy(this.gameObject);
				}
			}
			Vector3 blackHolePos = new Vector3(0,0,0);
			float distanceToCenter = Vector3.Distance(transform.position, blackHolePos);
			if (distanceToCenter < 10.0f) {
				this.GetComponent<Rigidbody2D>().velocity /= 1.01f;
				Vector3 blackHoleForce = transform.position - blackHolePos;
				GetComponent<Rigidbody2D>().AddForce(blackHoleForce.normalized * -10);
				if (soundHasPlayed == false) {
					blackHoleSound.Play();
					soundHasPlayed = true;
				}
			}
		}
	}



	IEnumerator springJ(GameObject thePlanet) {
		theSpringJoint.distance = thePlanet.transform.localScale.x * 3.5f;
		theSpringJoint.connectedBody = thePlanet.GetComponent<Rigidbody2D>();
		theSpringJoint.enabled = true;
		thisPlanet = thePlanet;
		yield return null;
	}

	IEnumerator waitNow() {
		yield return new WaitForSeconds(3);
		timeToDie = true;
	}
}
