using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	public Item item;
	public int myIndexInIventory;
	DragHandler dragHandler;
	Inventory inventory;
	EquipmentManager equipmentManager;

	void Start(){
		myIndexInIventory = transform.GetSiblingIndex ();
		dragHandler = DragHandler.GetInstance ();
		inventory = Inventory.GetInstance ();
		equipmentManager = EquipmentManager.GetInstance ();
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

	public void UseItem(){
		if (item != null) {
			item.Use (myIndexInIventory);
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
		dragHandler.inventorySlotBeingDraggedFrom = this;
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
		if (dragHandler.inventorySlotBeingDraggedFrom != null) {
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
		} else if (dragHandler.equipmentSlotBeingDraggedFrom != null) {
			Equipment nullNewItem = null, nullOldItem = null;
			Item temp = null;
			if (item != null)
				temp = item;
			if (inventory.items [myIndexInIventory] != null) {
				if ((int)((Equipment)inventory.items [myIndexInIventory]).equipSlot == (int)dragHandler.equipmentSlotBeingDraggedFrom.item.equipSlot) {
					inventory.items [myIndexInIventory] = dragHandler.equipmentSlotBeingDraggedFrom.item;
					equipmentManager.currentEquipment [dragHandler.equipmentSlotBeingDraggedFrom.myEquipmentSlotIndex] = (Equipment)temp; // needs to be tested with non-equipment items

					inventory.onItemChangedCallback.Invoke ();

					//Equipment nullNewItem = null, nullOldItem = null;
					equipmentManager.onEquipmentChangedCallback.Invoke (nullNewItem, nullOldItem);
				} else {
					return;
				}
			} else {
				inventory.items [myIndexInIventory] = dragHandler.equipmentSlotBeingDraggedFrom.item;
				equipmentManager.currentEquipment [dragHandler.equipmentSlotBeingDraggedFrom.myEquipmentSlotIndex] = (Equipment)temp; // needs to be tested with non-equipment items

				inventory.onItemChangedCallback.Invoke ();


				equipmentManager.onEquipmentChangedCallback.Invoke (nullNewItem, nullOldItem);
			}
		}
	}
}
