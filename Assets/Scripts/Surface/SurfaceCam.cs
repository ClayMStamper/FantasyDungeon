using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCam : MonoBehaviour {

	public Transform target;

	[Range(0,5)]
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	//public MapGenerator mapGenerator;
	//public Vector2 mapEdgeBumper;
	//Vector2 mapSize;

	void Start(){
		//mapSize.x = mapGenerator.mapSize.x;
		//mapSize.y = mapGenerator.mapSize.y;
	}

	void LateUpdate(){
		UpdateCameraPos ();
	}

	[ContextMenu("Update Cam Pos")]
	void UpdateCameraPos(){
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
		smoothedPosition.x = Mathf.Clamp (smoothedPosition.x, 0f, float.MaxValue);
		//smoothedPosition.z = Mathf.Clamp (smoothedPosition.z, -mapSize.y / 2 + mapEdgeBumper.y, mapSize.y / 2 - mapEdgeBumper.y);
		transform.position = smoothedPosition;
	}
}
