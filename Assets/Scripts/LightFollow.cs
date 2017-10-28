using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour {

	public Transform target;

	[Range(0,10)]
	public float smoothSpeed = 2;
	public Vector3 offset;

	void LateUpdate(){
		UpdateLightPos ();
	}

	[ContextMenu("Update Cam Pos")]
	void UpdateLightPos(){
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
		//smoothedPosition.x = Mathf.Clamp (smoothedPosition.x, 0, 52);
		//smoothedPosition.y = Mathf.Clamp (smoothedPosition.y, -30, 0);
		transform.position = smoothedPosition;
	}
}
