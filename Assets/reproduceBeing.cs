using UnityEngine;
using System.Collections;

public class reproduceBeing : MonoBehaviour {

	public float maxGravDist = 2.5f;
	public GameObject beingPrefab;
	public GameObject encapsulatedPrefab;
	public AudioSource collideSound;
	public AudioSource reproduceSound;

	public Sprite emptyEncapsulatedSprite;
	private SpriteRenderer spriteRenderer;

	private int oldNumBeings = 0;
	private int BeingsOnThisPlanet = 0;
	private bool planetsHaveSettled = false;
	private bool hasReproduced = false;
	GameObject[] beings;

	void Start() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100,100),Random.Range(-100,100)));
		StartCoroutine(makePlanetKinematic());
	}

	void OnCollisionEnter2D(Collision2D coll) {
		collideSound.Play();
		StartCoroutine(waitNow());
		//if there aren't any beings on this planet and if an encapsulated being hits this planet
		if (planetsHaveSettled == true) {
			Debug.Log("collided");
			coll.rigidbody.velocity /= 10;
			//coll.gameObject.renderer.material.color = Color.red;
			//change beingEncapsulated graphic to empty circle
			spriteRenderer = coll.gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = emptyEncapsulatedSprite;
			Vector3 planetForce = transform.position - coll.transform.position;
			GameObject beingClone = (GameObject)Instantiate(beingPrefab, coll.transform.position, Quaternion.identity);
			beingClone.GetComponent<Rigidbody2D>().AddForce(planetForce.normalized * 100);
			/*
			GameObject secondBeingClone = (GameObject)Instantiate(beingPrefab, transform.position, Quaternion.identity);
			secondBeingClone.rigidbody2D.AddForce(planetForce.normalized * 100);
			GameObject thirdBeingClone = (GameObject)Instantiate(beingPrefab, transform.position, Quaternion.identity);
			thirdBeingClone.rigidbody2D.AddForce(planetForce.normalized * 100);*/
			if (hasReproduced == false) {
				GameObject theText = GameObject.Find("Text");
				textPrompts prompts = theText.GetComponent<textPrompts>();
				if (prompts.showMessage == 5) {
					prompts.showMessage = 6;
				}
				StartCoroutine(reproduce());
			}
		}
	}


	//if the object that just got instantiated is within the radius of this planet
	void Update() {
		beings = GameObject.FindGameObjectsWithTag("being");
		if (beings.Length > oldNumBeings) {
			GameObject being = beings[beings.Length - 1];
			float dist = Vector3.Distance(being.transform.position, transform.position);
			if (dist <= maxGravDist) {
				BeingsOnThisPlanet += 1;
				//Debug.Log(BeingsOnThisPlanet);
				if (BeingsOnThisPlanet == 2) {
					Debug.Log ("reproduce!");
					//every x seconds add a clone, until BeingsOnThisPlanet reaches a certain number
					//start coroutine and use waitforseconds in ienumerator?
					if (hasReproduced == false) {
						StartCoroutine(reproduce());
					}
					//GameObject beingClone = (GameObject)Instantiate(beingPrefab, transform.position, Quaternion.identity);
					//beingClone.rigidbody2D.AddForce(new Vector2(Random.Range(-100,100),Random.Range(-100,100)));
				}
			}
			oldNumBeings = beings.Length;
			//Debug.Log (oldNumBeings);
		}
	}

	IEnumerator reproduce() {
		//audioSource = GameObject.Find("reproductionSound");
		//audioSource.audio.Play();
		hasReproduced = true;
		reproduceSound.Play ();
		GameObject theText = GameObject.Find("Text");
		textPrompts prompts = theText.GetComponent<textPrompts>();
		if (prompts.showMessage == 1) {
			prompts.showMessage = 2;
		}
		for (int i=0; i<40; i++) {
			yield return new WaitForSeconds(0.3f);
			GameObject beingClone = (GameObject)Instantiate(beingPrefab, transform.position, Quaternion.identity);
			beingClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100,100),Random.Range(-100,100)));
		}
		if (prompts.showMessage == 2) {
			prompts.showMessage = 3;
		}
		//encapsulated
		for (int i=0; i<10; i++) {
			Vector3 randoPos = new Vector3((transform.position.x + Random.Range(-1.5f, 1.5f)), (transform.position.y + Random.Range(-1.5f, 1.5f)), 0);
			GameObject encapsulatedClone = (GameObject)Instantiate(encapsulatedPrefab, randoPos, Quaternion.identity);
			//encapsulatedClone.rigidbody2D.AddForce(new Vector2(Random.Range(-1,1),Random.Range(-1,1)));
			yield return new WaitForSeconds(2);
		}
		if (prompts.showMessage == 3) {
			prompts.showMessage = 4;
		}
	}

	IEnumerator makePlanetKinematic() {
		yield return new WaitForSeconds(0.5f);
		GetComponent<Rigidbody2D>().isKinematic = true;
		CircleCollider2D circColl = transform.GetComponent<CircleCollider2D>();
		circColl.radius = 2.56f;
	}

	IEnumerator waitNow() {
		yield return new WaitForSeconds(1.0f);
		planetsHaveSettled = true;
	}

	/*
	beings = GameObject.FindGameObjectsWithTag("being");
	foreach (GameObject being in beings) {
		float dist = Vector3.Distance(being.transform.position, transform.position);
		if (dist <= maxGravDist) {
			numBeings += 1;
		}
	}
	Debug.Log(numBeings);*/
}
