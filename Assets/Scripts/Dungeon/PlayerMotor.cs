﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	public float screenEdgeBuffer;
	Animator anim;

	void Start(){
		anim = GetComponent <Animator> ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.down * turnSpeed);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.up * turnSpeed);
		} 

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
			anim.SetBool ("running", true);
			anim.speed = 1f;
		} else if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.back * Time.deltaTime * speed / 2);
			anim.SetBool ("running", true);
			anim.speed = .5f;
		} else {
			anim.SetBool ("running", false);
		}


		// keep within camera, even when on the edge
	//	Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint (Vector3.zero);
	//	Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

	//	transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minScreenBounds.x - screenEdgeBuffer,
	//		maxScreenBounds.x - screenEdgeBuffer), Mathf.Clamp (transform.position.y, minScreenBounds.y - screenEdgeBuffer,
	//		maxScreenBounds.y - screenEdgeBuffer), transform.position.z);
	}
}
