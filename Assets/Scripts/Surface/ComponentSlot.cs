using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentSlot : MonoBehaviour {

	public Image icon;

	public Item item;
	public int myIndexInIventory;
	DragHandler dragHandler;
	Inventory inventory;

//	EquipmentManager equipmentManager;

	void Start(){
		myIndexInIventory = transform.GetSiblingIndex ();
		dragHandler = DragHandler.GetInstance ();
		inventory = Inventory.GetInstance ();
		//equipmentManager = EquipmentManager.GetInstance ();
	}

	public void AddItemUI(Item newItem){
		item = newItem;	

		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void ClearSlotUI(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	public void MoveItemToForge(){
		if (item != null) {
			
		}
		ClearSlotUI ();
	}
	void ResetIcon(){
		icon.transform.localPosition = Vector3.zero;
		icon.transform.localScale = new Vector3 (1f, 1f, 1f);
		icon.GetComponent <Canvas> ().sortingOrder = 1;
	}

	public void Drag(){
		dragHandler.itemBeingDragged = item;
		dragHandler.inventorySlotBeingDraggedFrom = InventoryUI.GetInstance().slots[myIndexInIventory];
		dragHandler.equipmentSlotBeingDraggedFrom = null;
		icon.GetComponent <Canvas> ().sortingOrder = 2;
		icon.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
		icon.transform.position = Input.mousePosition;
	}

	public void EndDrag(){
		print ("Dropping: " + dragHandler.itemBeingDragged);
		ResetIcon ();
	}

	public void OnDrop(){
		/*Item temp = item;
		item = Inventory.GetInstance ().slotBeingDraggedFrom.item;
		Inventory.GetInstance ().slotBeingDraggedFrom.item = temp;*/
		int draggedFromIndex = -1; // -1 is an arbitrary value 
		int draggedToIndex = myIndexInIventory;
	
		for (int i = 0; i < Inventory.GetInstance ().space; i++) {
			if (InventoryUI.GetInstance ().slots [i] == dragHandler.inventorySlotBeingDraggedFrom) {
				draggedFromIndex = i;
			}
		}
		Item temp = Inventory.GetInstance ().items [draggedFromIndex];
		Inventory.GetInstance ().items [draggedFromIndex] = Inventory.GetInstance ().items [draggedToIndex];
		Inventory.GetInstance ().items [draggedToIndex] = temp;
	
		Inventory.GetInstance ().onItemChangedCallback.Invoke ();
		
	}
}
