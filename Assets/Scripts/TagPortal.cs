using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPortal : Viewable {

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		
	}
}
