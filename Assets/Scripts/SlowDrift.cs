using UnityEngine;
using System.Collections;

public class SlowDrift : MonoBehaviour {

	private Vector3 initialPos;

	private float xDriftSpeed;
	private float yDriftSpeed;

	public float xMaxDist = 1f;
	public float yMaxDist = 1f;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
		xDriftSpeed = Random.value/5f * Mathf.Sign(Random.value-0.5f);
		yDriftSpeed = Random.value/5f * Mathf.Sign(Random.value-0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(xDriftSpeed, yDriftSpeed, 0f)*Time.deltaTime;

		if (Mathf.Abs(transform.position.y - initialPos.y) > yMaxDist) {
			yDriftSpeed = -yDriftSpeed;
		}

		if (Mathf.Abs(transform.position.x - initialPos.x) > xMaxDist) {
			xDriftSpeed = -xDriftSpeed;
		}
	}
}
