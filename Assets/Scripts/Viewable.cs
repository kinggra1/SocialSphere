using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewable : MonoBehaviour {

	protected Vector3 originalPos;
	protected bool beingViewed = false;

	protected float viewTime = 0f;

	void Start() {

	}

	public void Update() {
		if (beingViewed) {
			viewTime += Time.deltaTime;
		}
	}

	public void LookedAt() {
		if (gameObject.activeInHierarchy) {
			StopAllCoroutines();
			StartCoroutine("Grow");
		}
	}

	public void LookedAway() {
		if (gameObject.activeInHierarchy) {
			StopAllCoroutines();
			StartCoroutine("Shrink");
		} else {
			transform.position = originalPos;
			transform.localScale = Vector3.one;
		}
	}

	public void Reappear() {
		if (!gameObject.activeInHierarchy) {
			gameObject.SetActive(true);
		}

		StopAllCoroutines();
		StartCoroutine("Shrink"); // because this goes to original pos/scale
	}

	public void Disappear() {
		StopAllCoroutines();
		StartCoroutine("Flee");
	}


	IEnumerator Grow() {
		viewTime = 0f;
		beingViewed = true;

		float timer = 0f;
		float duration = 0.5f;

		float lastScale = transform.localScale.x;
		Vector3 lastPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * (lastScale + (1.4f-lastScale)*(timer/duration));
			transform.position = Vector3.Lerp(lastPos, originalPos + -transform.forward*3f, timer/duration);
			yield return null;
		}
	}

	IEnumerator Shrink() {
		beingViewed = false;

		float timer = 0f;
		float duration = 0.5f;

		float lastScale = transform.localScale.x;
		Vector3 lastPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * (lastScale - (lastScale-1f)*(timer/duration));
			transform.position = Vector3.Lerp(lastPos, originalPos, timer/duration);
			yield return null;
		}
	}

	IEnumerator Flee() {
		float timer = 0f;
		float duration = 0.5f;

		float lastScale = transform.localScale.x;
		Vector3 lastPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * (lastScale - lastScale*0.9f*(timer/duration));
			transform.position = Vector3.Lerp(lastPos, originalPos+transform.forward*10f, timer/duration);
			yield return null;
		}

		gameObject.SetActive(false);
	}
}
