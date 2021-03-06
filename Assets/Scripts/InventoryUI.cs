﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	Inventory inventory;
	public Transform slotsParent;
	public GameObject inventoryUI;

	public InventorySlot[] slots;

	#region Singelton
	private static InventoryUI instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

	}

	public static InventoryUI GetInstance(){
		return instance;
	}
	#endregion

	void Start () {
		inventory = Inventory.GetInstance ();
		inventory.onItemChangedCallback += UpdateUI;
//		inventory.onItemChangedCallback += LateUpdateUI;

		slots = slotsParent.GetComponentsInChildren <InventorySlot> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryUI.SetActive (!inventoryUI.activeSelf);
		}
	}

	public void UpdateUI(){
		print ("updating UI");
		for (int i = 0; i < slots.Length; i++) {
			if (inventory.items [i] != null) {
				slots [i].AddItemUI (inventory.items [i]);
			} else {
				slots [i].ClearSlotUI ();
			}
		}
		foreach (InventorySlot slot in slots) {
			if (slot.item != null) {
				slot.icon.sprite = slot.item.icon;
			} else {
				slot.icon.sprite = null;
			}
		}
	}

}
