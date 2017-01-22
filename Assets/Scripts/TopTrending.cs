using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTrending : MonoBehaviour {

	public GameObject trendPrefab;
	public SocialSphere sphere;

	private List<TrendPortal> portals = new List<TrendPortal>();

	// Use this for initialization
	void Start () {
		TwitterAPI.instance.GetTopTrends(PopulateTrending);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PopulateTrending(List<TweetTopTrendsData> trends) {
		float distance = 25;

		float minRot = 45f*Mathf.PI/180;
		float maxRot = 135f*Mathf.PI/180;

		int count = Mathf.Min(10, trends.Count);
		for (int i = 0; i < count; i++) {
			float rad = Mathf.PI - minRot - (i*(maxRot-minRot))/(count-1);

			float yOffset = Mathf.Cos(rad)*distance;
			float xOffset = Mathf.Sin(rad)*distance;

			Vector3 trendPos = transform.position + new Vector3(xOffset, yOffset, 0f);

			GameObject trendObj = Instantiate(
				trendPrefab,
				trendPos,
				Quaternion.LookRotation(trendPos - Camera.main.transform.position)
			) as GameObject;

			TextMesh text = trendObj.GetComponent<TextMesh>();
			text.text = trends[i].name;
			trendObj.AddComponent<BoxCollider>();
			//trendObj.AddComponent<SlowDrift>();

			TrendPortal portal = trendObj.GetComponent<TrendPortal>();
			portal.SetSphere(sphere);
			portal.SetTrendGenerator(this);

			portals.Add(portal);
		}
	}

	public void Disappear() {
		foreach (TrendPortal trend in portals) {
			StartCoroutine(trend.Hide());
		}
	}

	public void Show() {
		foreach (TrendPortal trend in portals) {
			StartCoroutine(trend.Hide());
		}
	}
}
