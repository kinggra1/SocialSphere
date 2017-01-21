using UnityEngine;
using System.Collections;

public class SlowDrift : MonoBehaviour {

	private Vector3 initialPos;

	private float xDriftSpeed;
	private float yDriftSpeed;

	public float xMaxDist = 10f;
	public float yMaxDist = 10f;

	// Use this for initialization
	void Awake () {
		initialPos = transform.localPosition;
		xDriftSpeed = Random.value/5f * Mathf.Sign(Random.value-0.5f);
		yDriftSpeed = Random.value/5f * Mathf.Sign(Random.value-0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition += new Vector3(xDriftSpeed, yDriftSpeed, 0f) * Time.deltaTime;

		if (Mathf.Abs(transform.localPosition.y - initialPos.y) > yMaxDist) {
			yDriftSpeed = -yDriftSpeed;
		}

		if (Mathf.Abs(transform.localPosition.x - initialPos.x) > xMaxDist) {
			xDriftSpeed = -xDriftSpeed;
		}
	}
}
