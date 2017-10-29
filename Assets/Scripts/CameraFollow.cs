using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	[Range(0,1)]
	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void LateUpdate(){
		UpdateCameraPos ();
	}

	[ContextMenu("Update Cam Pos")]
	void UpdateCameraPos(){
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
		smoothedPosition.x = Mathf.Clamp (smoothedPosition.x, -26, 26);
		smoothedPosition.z = Mathf.Clamp (smoothedPosition.z, -15, 15);
		transform.position = smoothedPosition;
	}
}
