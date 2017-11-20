using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour {

	public Item itemBeingDragged;
	public InventorySlot inventorySlotBeingDraggedFrom;
	public EquippedSlot equipmentSlotBeingDraggedFrom;

	Inventory inventory;
	EquipmentManager equipmentManager;

	#region Singleton
	private static DragHandler instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

	}

	public static DragHandler GetInstance(){
		return instance;
	}
	#endregion

	void Start(){
		inventory = Inventory.GetInstance ();
		equipmentManager = EquipmentManager.GetInstance ();
	}

	public void OnDestroyItem(){
		//print ("destroying");
		if (inventorySlotBeingDraggedFrom != null) {
			inventory.items [inventorySlotBeingDraggedFrom.myIndexInIventory] = null;
			inventory.onItemChangedCallback.Invoke ();
		} else if (equipmentSlotBeingDraggedFrom != null) {
			equipmentManager.currentEquipment [equipmentSlotBeingDraggedFrom.myEquipmentSlotIndex] = null;
			Equipment nullNewItem = null, nullOldItem = null;
			equipmentManager.onEquipmentChangedCallback.Invoke (nullNewItem, nullOldItem);
		}
	}

	public void OnDragToEquip(){
		if (inventorySlotBeingDraggedFrom != null) {
			equipmentManager.Equip ((Equipment)inventory.items [inventorySlotBeingDraggedFrom.myIndexInIventory]); // need to test non equipment items
			inventory.items [inventorySlotBeingDraggedFrom.myIndexInIventory] = null;
			inventory.onItemChangedCallback.Invoke ();
		} 
	}

}
