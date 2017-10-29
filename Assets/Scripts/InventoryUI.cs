using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	Inventory inventory;
	public Transform slotsParent;
	public GameObject inventoryUI;

	InventorySlot[] slots;

	void Start () {
		inventory = Inventory.GetInstance ();
		inventory.onItemChangedCallback += UpdateUI;
		inventory.onItemChangedCallback += LateUpdateUI;

		slots = slotsParent.GetComponentsInChildren <InventorySlot> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryUI.SetActive (!inventoryUI.activeSelf);
		}
	}

//	[ContextMenu("Update Inventory")]
	public void UpdateUI(){
//		print ("updating UI");
		for (int i = 0; i < slots.Length; i++) {
			if (i < inventory.items.Count) {
				slots [i].AddItem (inventory.items [i]); 
			} else {
				slots [i].ClearSlot ();
			}
		}
	}
	void LateUpdateUI(){
		//intended to occur at the very end of onItemChangedCallback
		Invoke ("UpdateUI", 0.01f);
	}
}
