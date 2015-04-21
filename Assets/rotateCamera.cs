using UnityEngine;
using System.Collections;

public class rotateCamera : MonoBehaviour {

	private int rotationSpeed = 5;

	void Update () {
		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.World);
	}
}
