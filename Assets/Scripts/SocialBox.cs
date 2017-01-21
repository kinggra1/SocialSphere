using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocialBox : MonoBehaviour {

	public GameObject tagParent;
	public GameObject tagPrefab;

	// Use this for initialization
	void Start () {
		List<string> tags = new List<string>();
		tags.Add("#tag1");
		tags.Add("#tag2");
		//tags.Add("#tag3");
		//tags.Add("#tag4");
		//tags.Add("#tag5");
		//tags.Add("#tag1");
		tags.Add("#tag2");
		tags.Add("#tag3");
		tags.Add("#tag4");
		tags.Add("#tag5");
		AddTags(tags);
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

			float xPos = Mathf.Cos(rad)*8;
			float yPos = Mathf.Sin(rad)*4;

			tag.transform.position += new Vector3(xPos, yPos, 0f);
		}
	}
}
