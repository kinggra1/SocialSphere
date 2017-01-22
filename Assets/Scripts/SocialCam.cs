using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialCam : MonoBehaviour {

	public GameObject centerEye;

	private Viewable pViewable = null;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {



		// UNTIL WE GOT SOME HARDWARE THAT CAN RUN OCULUS, DO DIS SHIT
		float hInput = Input.GetAxis("Mouse X");
		float vInput = Input.GetAxis("Mouse Y");
		if (hInput!= 0f) {
			transform.Rotate(0f, hInput, 0f, Space.World);
		}
		if (vInput != 0f) {
			transform.Rotate(-vInput, 0f, 0f, Space.Self);
		}
		// END OF DUMB MOTION WITHOUT OCULUS STUFF



		Debug.DrawRay(centerEye.transform.position, centerEye.transform.forward*100f, Color.red);

		RaycastHit hitInfo;
		if (Physics.Raycast(centerEye.transform.position, centerEye.transform.forward, out hitInfo)) {
			Viewable viewable = hitInfo.collider.gameObject.GetComponent<Viewable>();
			if (viewable == null) {
				// check in self if not in parent (for tags)
				viewable = hitInfo.collider.gameObject.GetComponentInParent<Viewable>();
			}

			if (viewable != null) {

				if (viewable != pViewable) {
					if (pViewable != null) {
						pViewable.LookedAway();
					}
					viewable.LookedAt();
					pViewable = viewable;
				}
			}
		} else if (pViewable != null) {
			pViewable.LookedAway();
			pViewable = null;
		}


	}
}
