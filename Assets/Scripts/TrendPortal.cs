using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendPortal : Viewable {

	private SocialSphere sphere;
	private TopTrending generator;
	private TextMesh text;

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();
		originalPos = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		base.Update();
		if (viewTime > 2f) {
			Debug.Log("Searching for keyword: " + text.text);
			sphere.gameObject.SetActive(true);
			sphere.Apperate();
			sphere.SearchAndFill(text.text);
			generator.Disappear();

			viewTime = 0f;
			beingViewed = false;
		}
	}

	public void SetTrendGenerator(TopTrending parent) {
		generator = parent;
	}

	public void SetSphere(SocialSphere master) {
		sphere = master;
	}

	public IEnumerator Show() {
		gameObject.SetActive(true);

		float timer = 0f;
		float duration = 0.5f;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * timer/duration;
			yield return null;
		}

		transform.localScale = Vector3.one;
	}

	public IEnumerator Hide() {
		float timer = 0f;
		float duration = 0.5f;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.localScale = Vector3.one * (1 - timer/duration);
			yield return null;
		}

		transform.localScale = Vector3.zero;
		gameObject.SetActive(false);
	}
}
