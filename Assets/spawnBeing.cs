using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnBeing : MonoBehaviour {
	public GameObject beingPrefab;
	public textPrompts prompts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0; //makes sure Z = 0
			transform.position = pos;
			GetComponent<AudioSource>().Play();
			GameObject beingClone = (GameObject)Instantiate(beingPrefab, pos, Quaternion.identity);
			GetComponent<AudioSource>().Play();
			if (prompts.showMessage == 0) {
				prompts.showMessage = 1;
			}
		}
	}
}
