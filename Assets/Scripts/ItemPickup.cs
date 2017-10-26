using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable {

	public Item item;

	void Start(){
		GetComponent <SpriteRenderer> ().sprite = item.icon;
	}

	public override void Interact(Collider2D col){
		base.Interact (col);

		Pickup ();
	}

	void Pickup(){
	//	Debug.Log ("picking up " + item.name);
		bool wasPickedUp = Inventory.GetInstance ().Add (item);
		if (wasPickedUp)
			Destroy (gameObject);
	}
}
