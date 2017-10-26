using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;

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
