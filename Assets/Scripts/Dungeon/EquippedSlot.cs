using UnityEngine.UI;
using UnityEngine;

public class EquippedSlot : MonoBehaviour {

	public Equipment item;
	public Image icon;
	public EquipmentSlot slotType;
	public int myEquipmentSlotIndex;

	EquipmentManager equipmentManager;
	DragHandler dragHandler;

	void Start (){
		myEquipmentSlotIndex = transform.GetSiblingIndex ();
		equipmentManager = EquipmentManager.GetInstance ();
		dragHandler = DragHandler.GetInstance ();
	}

	public void Equip(Equipment newItem){
		if (item != null) {
			ClearSlot();
		}
		item = newItem;

		icon.sprite = newItem.icon;
		icon.enabled = true;
	//	print ("Equipped " + newItem);
		//Inventory.GetInstance ().onItemChangedCallback.Invoke ();
		//FindObjectOfType <InventoryUI>().UpdateUI();
	}

	public void ClearSlot(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	//	print ("Cleared : " + name);
		//EquipmentManager.GetInstance ().currentEquipment [currentEquipmentIndex] = null;
		//Inventory.GetInstance ().onItemChangedCallback.Invoke ();
	}

	public void OnDestroyItem(){
	//	Inventory.GetInstance ().Remove (item);
	}

	public void Unequip(){
		if (item != null) {
		//	print ("Unequipping " + item);
			//Inventory.GetInstance ().Add (item);
			equipmentManager.Unequip (myEquipmentSlotIndex);
			ClearSlot ();
			//Inventory.GetInstance ().onItemChangedCallback.Invoke ();
		}
	}

	void ResetIcon(){
		icon.transform.localPosition = Vector3.zero;
		icon.transform.localScale = new Vector3 (1f, 1f, 1f);
		icon.GetComponent <Canvas> ().sortingOrder = 1;
	}

	public void OnDrag(){
		dragHandler.itemBeingDragged = item;
		dragHandler.equipmentSlotBeingDraggedFrom = this;
		dragHandler.inventorySlotBeingDraggedFrom = null;
		icon.GetComponent <Canvas> ().sortingOrder = 2;
		icon.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
		icon.transform.position = Input.mousePosition;
	}

	public void EndDrag(){
		print ("Dropping: " + dragHandler.itemBeingDragged);
		ResetIcon ();
	}

	public void OnDrop(){
		dragHandler.OnDragToEquip ();
	}
}
