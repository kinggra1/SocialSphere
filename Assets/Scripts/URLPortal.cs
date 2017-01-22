using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLPortal : Viewable {

	public SocialSphere sphere;
	public SocialBox box;
	private TextMesh text;

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();

		originalPos = transform.localPosition;
		transform.localPosition = Vector3.zero;
		LookedAway();
	}

	// Update is called once per frame
	void Update () {
		base.Update();
		if (viewTime > 2f) {
			Debug.Log("Searching for keyword: " + text.text);
			sphere.SearchAndFill("from:"+text.text.Substring(1));
			box.ReturnToSphere();

			viewTime = 0f;
			beingViewed = false;
		}
	}

	public void SetBox(SocialBox parent) {
		box = parent;
	}

	public void SetSphere(SocialSphere master) {
		sphere = master;
	}
}
