using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialSphere : MonoBehaviour {

	public GameObject previewPrefab;

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


			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
