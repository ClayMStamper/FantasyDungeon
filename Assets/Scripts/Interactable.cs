using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		Interact (col);
	}

	public virtual void Interact(Collider2D col){
		print ("Interacting with " + col);
	}
}
