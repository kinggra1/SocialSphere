using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialSphere : MonoBehaviour {

	public GameObject previewPrefab;

	private List<PreviewBox> previewBoxes = new List<PreviewBox>();
	private List<TweetSearchTwitterData> tweets;
	private int tweetIndex = 0;

	// Use this for initialization
	void Start () {

		float minPhi = 50f*Mathf.PI/180;
		float maxPhi = 130f*Mathf.PI/180;


		float distance = 20f;

		for (int r = 0; r < 5; r++) {
			float theta = minPhi + r*(maxPhi-minPhi)/4;

			int iMax = 10-Mathf.Abs(r-2)*2;
			for (int i = 0; i < iMax; i++) {
				float phi = (i*2*Mathf.PI)/iMax;

				float xPos = Mathf.Sin(theta) * Mathf.Cos(phi) * distance;
				float yPos = Mathf.Cos(theta) * distance;
				float zPos = Mathf.Sin(theta) * Mathf.Sin(phi) * distance;

				Vector3 previewPos = new Vector3(xPos, yPos, zPos);
				GameObject preview = Instantiate(
					previewPrefab,
					new Vector3(xPos, yPos, zPos),
					Quaternion.LookRotation(previewPos - Camera.main.transform.position)
				) as GameObject;

				PreviewBox pBox = preview.GetComponent<PreviewBox>();
				pBox.SetSphere(this);
				previewBoxes.Add(pBox);


			}
		}

		TwitterAPI.instance.SearchTwitter("word", PopulateTweets);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PopulateTweets(List<TweetSearchTwitterData> newTweets) {
		Debug.Log(newTweets.Count + " tweets pulled");
		tweets = newTweets;

		foreach (PreviewBox box in previewBoxes) {
			box.SetTweet(NextTweet());
		}
	}

	public TweetSearchTwitterData NextTweet() {
		TweetSearchTwitterData result = tweets[tweetIndex];
		tweetIndex++;
		tweetIndex%=tweets.Count;
		return result;
	}
}
