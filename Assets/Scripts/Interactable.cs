using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		Interact (col);
	}

	public virtual void Interact(Collider col){
		print ("Interacting with " + col);
	}
}
