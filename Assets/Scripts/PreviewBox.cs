using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PreviewBox : Viewable {

	private GameObject centerEye;
	private bool seen = false;

	private TweetSearchTwitterData tweet = null;

	private Text text;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
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

		if (viewTime > 2f) {
			Destroy(this.gameObject);
		}
	}

	public void Refresh() {
		if (text != null) {
			text.text = "THIS HAS BEEN SEEN";
			seen = false;
		}
	}

	public void SetTweet(TweetSearchTwitterData newTweet) {
		tweet = newTweet;
		text.text = tweet.tweetText;
	}
}
