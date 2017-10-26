using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = GetComponent <Animator> ();
	}

	void Update(){
		if (Input.GetKey (KeyCode.Space)){
			Attack ();
		}
	}

	public virtual void Attack(){
		print ("Attacking");
		anim.SetTrigger ("attack");
	}

}
