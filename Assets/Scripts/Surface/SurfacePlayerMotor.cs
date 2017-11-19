using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfacePlayerMotor : MonoBehaviour {

	public float speed = 3f;
	public float animTransitionSpeed = .2f;
	public SurfaceCam camera;
	Animator anim;

	void Start(){
		anim = GetComponent <Animator> ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			if (camera.offset.x > 0)
				camera.offset.x = -camera.offset.x;
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 180, transform.localRotation.z);
			transform.Translate (Vector3.right * Time.deltaTime * speed);
			anim.SetFloat ("Blend", anim.GetFloat ("Blend") + animTransitionSpeed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.D)) {
			if (camera.offset.x < 0)
				camera.offset.x = -camera.offset.x;
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 0, transform.localRotation.z);
			transform.Translate (Vector3.right * Time.deltaTime * speed);
			anim.SetFloat ("Blend", anim.GetFloat ("Blend") + animTransitionSpeed * Time.deltaTime);
		//	anim.SetBool ("running", true);
		//	anim.speed = .5f;
		} else {
			anim.SetFloat ("Blend", 0f);
		}
	}
}