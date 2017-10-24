using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turnSpeed;

	void LateUpdate(){
		//transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	void Update () {
		if (Input.GetKey (KeyCode.A)){
			transform.Rotate (Vector3.forward * turnSpeed);
		}
		if (Input.GetKey (KeyCode.W)){
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.D)){
			transform.Rotate (Vector3.back *  turnSpeed);
		}
		if (Input.GetKey (KeyCode.S)){
			transform.Translate (Vector3.down * Time.deltaTime *speed / 2);
		}

		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint (Vector3.zero);
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minScreenBounds.x,
			maxScreenBounds.x), Mathf.Clamp (transform.position.y, minScreenBounds.y,
			maxScreenBounds.y), transform.position.z);
	}
}
