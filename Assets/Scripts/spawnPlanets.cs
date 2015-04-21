using UnityEngine;
using System.Collections;

public class spawnPlanets : MonoBehaviour {
	public GameObject planetPrefab;


	void Start () {
		for (int i=0; i<10; i++) {
			Vector3 position = new Vector3(Random.Range(-28.0F, 28.0F), Random.Range(-28.0F, 28.0F), 0);
			GameObject planetClone = (GameObject)Instantiate(planetPrefab, position, Quaternion.identity);
			Vector3 planetScale = planetClone.transform.localScale;
			planetScale.x = Random.Range(0.75f, 1.25f);
			planetScale.y = planetScale.x;
			planetClone.transform.localScale = planetScale;
			//planetClone.rigidbody2D.AddForce(new Vector2(Random.Range(-10,10),Random.Range(-10,10)));
		}
	}
}
