using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SocialBox : MonoBehaviour {

	public GameObject tagParent;
	public GameObject tagPrefab;
	private TweetSearchTwitterData tweet;
	private Text text;

	// Use this for initialization
	void Awake () {
		text = GetComponentInChildren<Text>();

	}

	void Start() {
		StartCoroutine("PresentSelf");
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
	
	}

	public void AddTags(List<string> tags) {
		for (int i = 0; i < tags.Count; i++) {
			float rad = Mathf.PI/2 + (i*2*Mathf.PI)/tags.Count;

			GameObject tag = Instantiate(tagPrefab, 
				tagParent.transform.position, 
				tagParent.transform.rotation,
				tagParent.transform) as GameObject;

			TextMesh text = tag.GetComponent<TextMesh>();
			text.text = tags[i];

			float xOffset = Mathf.Cos(rad)*16;
			float yOffset = Mathf.Sin(rad)*8;

			tag.transform.position += transform.right*xOffset + transform.up*yOffset;
		}
	}

	public void SetTweet(TweetSearchTwitterData newTweet) {
		if (newTweet == null) {
			return;
		}

		tweet = newTweet;
		text.text = tweet.tweetText;

		List<string> tags = new List<string>();
		foreach (string word in text.text.Split(' ')) {
			if (!SocialSphere.StopWord(word)) {
				tags.Add(word);
			}
		}

		AddTags(tags);
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
}
