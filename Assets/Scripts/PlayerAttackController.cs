using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

	void Update(){
		if (Input.GetKey (KeyCode.Space)){
			Attack ();
		}
	}

	public virtual void Attack(){
		print ("Attacking");
	}

}
