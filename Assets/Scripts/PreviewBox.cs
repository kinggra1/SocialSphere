using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PreviewBox : Viewable {

	private SocialSphere sphere;
	private GameObject centerEye;
	private bool seen = false;

	private TweetSearchTwitterData tweet = null;

	private Text text;

	// Use this for initialization
	void Start () {
		originalPos = transform.localPosition;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		SocialCam cam = player.GetComponent<SocialCam>();

		if (cam != null) {
			centerEye = cam.centerEye;
		} else {
			Debug.LogError("PREVIEW BOX COULD NOT FIND SOCIAL CAM");
		}

		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		float angle = Vector3.Angle(transform.position - centerEye.transform.position, centerEye.transform.forward);
		if (angle < 45f) {
			seen = true;
		} else if (seen && angle > 90f) {
			Refresh();
		}

		if (viewTime > 3f) {
			ShowSocialBox();
			sphere.HideFam();
		}
	}

	public void Refresh() {
		if (text != null) {
			SetTweet(sphere.NextTweet());
			seen = false;
		}
	}

	public void ShowSocialBox() {
		beingViewed = false;
		viewTime = 0f;

		GameObject box = Instantiate(
			sphere.socialPrefab,
			transform.position,
			transform.rotation
		);

		box.transform.localScale = transform.localScale;

		SocialBox socialBox = box.GetComponent<SocialBox>();
		socialBox.SetSphere(sphere);
		socialBox.SetTweet(tweet);
	}

	public void SetTweet(TweetSearchTwitterData newTweet) {
		tweet = newTweet;
		text.text = tweet.tweetText;
		StartCoroutine(Bob());
	}

	public void SetSphere(SocialSphere parent) {
		sphere = parent;
	}

	IEnumerator Bob() {
		float timer = 0f;
		float duration = 0.8f;

		Vector3 lastPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.position = lastPos + -transform.up * Mathf.Sin(Mathf.PI*(timer/duration));
			yield return null;
		}
	}
}
