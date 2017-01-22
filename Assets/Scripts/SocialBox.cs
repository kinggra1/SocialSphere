﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SocialBox : MonoBehaviour {

	public GameObject tagParent;
	public GameObject tagPrefab;
	private TweetSearchTwitterData tweet;
	private Text text;

	private GameObject centerEye;
	private SocialSphere sphere;

	// Use this for initialization
	void Awake () {
		text = GetComponentInChildren<Text>();

	}

	void Start() {
		StartCoroutine("PresentSelf");

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		SocialCam cam = player.GetComponent<SocialCam>();

		if (cam != null) {
			centerEye = cam.centerEye;
		} else {
			Debug.LogError("PREVIEW BOX COULD NOT FIND SOCIAL CAM");
		}
		//List<string> tags = new List<string>();
		//tags.Add("#tag1");
		//tags.Add("#tag2");
		//tags.Add("#tag3");
		//tags.Add("#tag4");
		//tags.Add("#tag5");
		//tags.Add("#tag1");
		//tags.Add("#tag2");
		//tags.Add("#tag3");
		//tags.Add("#tag4");
		//tags.Add("#tag5");
		//AddTags(tags);
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Vector3.Angle(transform.position - centerEye.transform.position, centerEye.transform.forward);
		if (angle > 80f) {
			ReturnToSphere();
		}
	}

	public void AddTags(List<string> tags) {
		for (int i = 0; i < tags.Count; i++) {
			float rad = Mathf.PI/2 + (i*2*Mathf.PI)/tags.Count;

			float xOffset = Mathf.Cos(rad)*16;
			float yOffset = Mathf.Sin(rad)*8;

			GameObject tag = Instantiate(tagPrefab, 
				tagParent.transform.position + transform.right*xOffset + transform.up*yOffset, 
				tagParent.transform.rotation,
				tagParent.transform) as GameObject;

			TextMesh text = tag.GetComponent<TextMesh>();
			TagPortal tagPortal = tag.GetComponent<TagPortal>();
			tagPortal.SetSphere(sphere);
			tagPortal.SetBox(this);

			tag.transform.rotation = Quaternion.LookRotation(tag.transform.position - Camera.main.transform.position);
			text.text = tags[i];
			tag.AddComponent<BoxCollider>();
		}
	}

	public void ReturnToSphere() {
		sphere.ShowFam();
		StartCoroutine("SayByeBye");
	}

	public void SetTweet(TweetSearchTwitterData newTweet) {
		if (newTweet == null) {
			return;
		}

		tweet = newTweet;
		text.text = tweet.tweetText;

		List<string> tags = new List<string>();
		foreach (string word in text.text.Split(' ')) {
            string stripWord = StripWord(word);
			if (!SocialSphere.StopWord(stripWord) && !stripWord.StartsWith("http")) {
				tags.Add(stripWord);
			}
		}

		AddTags(tags);
	}

	public void SetSphere(SocialSphere parent) {
		sphere = parent;
	}

	IEnumerator PresentSelf() {
		float duration = 1.0f;
		float timer = 0f;
		float travelDistance = -10f;

		Vector3 initialPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.position = Vector3.Lerp(
				initialPos, initialPos + transform.forward*travelDistance, timer/duration
			);

			yield return null;
		}
	}

	IEnumerator SayByeBye() {
		float duration = 1.0f;
		float timer = 0f;
		float travelDistance = 20f;

		Vector3 initialPos = transform.position;

		while (timer < duration) {
			timer += Time.deltaTime;
			transform.position = Vector3.Lerp(
				initialPos, initialPos + transform.forward*travelDistance, timer/duration
			);

			yield return null;
		}
		Destroy(gameObject);
	}

    string StripWord(string word)
    {
        char[] RemoveChars = { '!', ',', '"', '.', '?', '\'', '(', ')', '*'};

        word = word.Trim(RemoveChars);

        return word;
    }
}
