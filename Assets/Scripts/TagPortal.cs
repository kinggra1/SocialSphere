using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPortal : Viewable {

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		transform.localPosition = Vector3.zero;
		LookedAway();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}
}
