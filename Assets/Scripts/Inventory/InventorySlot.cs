﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	Item item;

	public void AddItem(Item newItem){
		item = newItem;	

		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void ClearSlot(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	public void OnDestroyItem(){
		Inventory.GetInstance ().Remove (item);
	}

	public void UseItem(){
		if (item != null) {
			item.Use ();
		}
		ClearSlot ();
	}
}
