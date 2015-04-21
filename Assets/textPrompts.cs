using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textPrompts : MonoBehaviour {

	public CanvasGroup fadeCanvasGroup;
	public Text theText;
	public int currentMessage = 0;
	public int showMessage = 0;
	private bool displayMessageHasStarted = false;

	void Start() {
		StartCoroutine(DisplayMessage(1.0f, "click to add beings"));
	}

	void Update() {
		if (currentMessage == 0 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "click to add beings"));
		}
		if (currentMessage == 1 && showMessage == 1 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "two or more beings will reproduce"));
		}
		if (currentMessage == 2 && showMessage == 2 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "as the population grows, they become more advanced"));
		}
		if (currentMessage == 3 && showMessage == 3 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "some beings evolve to overcome gravity and reach orbit"));
		}
		if (currentMessage == 4 && showMessage == 4 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "clicking on an orbital being launches it into space"));
		}
		if (currentMessage == 5 && showMessage == 5 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, "they can now colonize the galaxy"));
		}
		if (currentMessage == 6 && showMessage == 6 && displayMessageHasStarted == false) {
			StartCoroutine(DisplayMessage(1.0f, ""));
		}
	}

	public IEnumerator DisplayMessage(float speed, string message) {
		displayMessageHasStarted = true;
		currentMessage += 1;
		while (fadeCanvasGroup.alpha > 0f) {
			fadeCanvasGroup.alpha -= speed * Time.deltaTime;
			yield return null;
		}
		//yield return new WaitForSeconds(1);
		theText.text = message;
		while (fadeCanvasGroup.alpha < 1f) {
			fadeCanvasGroup.alpha += speed * Time.deltaTime;
			yield return null;
		}
		displayMessageHasStarted = false;
	}
}
