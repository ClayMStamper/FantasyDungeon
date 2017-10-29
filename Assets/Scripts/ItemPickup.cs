using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable {

	public Item item;

	void Start(){
		if (!GetComponent <SpriteRenderer> ()) {
			gameObject.AddComponent <SpriteRenderer> ();
		}
		GetComponent <SpriteRenderer> ().sprite = item.icon;

	}

	public override void Interact(Collider col){
		base.Interact (col);

		Pickup ();
	}

	void Pickup(){
	//	Debug.Log ("picking up " + item.name);
		bool wasPickedUp = Inventory.GetInstance ().Add (item);
		if (wasPickedUp)
			Destroy (gameObject);
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere (transform.position, .3f);
	}
}
