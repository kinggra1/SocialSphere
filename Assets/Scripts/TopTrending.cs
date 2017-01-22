using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTrending : MonoBehaviour {

	public GameObject trendPrefab;

	// Use this for initialization
	void Start () {
		TwitterAPI.instance.GetTopTrends(PopulateTrending);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PopulateTrending(List<TweetTopTrendsData> trends) {
		float distance = 20f;

		for (int i = 0; i < trends.Count; i++) {
			float rad = Mathf.PI/2 + (i*2*Mathf.PI)/trends.Count;

			float xOffset = Mathf.Cos(rad)*distance;
			float zOffset = Mathf.Sin(rad)*distance;

			Vector3 trendPos = transform.position + new Vector3(xOffset, 0f, zOffset);

			GameObject trendObj = Instantiate(
				trendPrefab,
				trendPos,
				Quaternion.LookRotation(Camera.main.transform.position - trendPos)
			) as GameObject;

			TextMesh text = trendObj.GetComponent<TextMesh>();
			text.text = trends[i].name;
			trendObj.AddComponent<BoxCollider>();
			trendObj.AddComponent<SlowDrift>();
		}
	}
}
