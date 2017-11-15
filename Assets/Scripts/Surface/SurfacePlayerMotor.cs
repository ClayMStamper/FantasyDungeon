using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfacePlayerMotor : MonoBehaviour {

	public float speed = 3f;

	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		//	anim.SetBool ("running", true);
		//	anim.speed = 1f;
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		//	anim.SetBool ("running", true);
		//	anim.speed = .5f;
		} else {
		//	anim.SetBool ("running", false);
		}
	}
}