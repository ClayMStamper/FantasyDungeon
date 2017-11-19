using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	public Item item;

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

	void ResetIcon(){
		icon.transform.localPosition = Vector3.zero;
		icon.transform.localScale = new Vector3 (1f, 1f, 1f);
		icon.GetComponent <Canvas> ().sortingOrder = 1;
	}

	public void Drag(){
		Inventory.GetInstance ().itemBeingDragged = item;
		Inventory.GetInstance ().slotBeingDraggedFrom = this;
		icon.GetComponent <Canvas> ().sortingOrder = 2;
		icon.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
		icon.transform.position = Input.mousePosition;
	}

	public void EndDrag(){
		print ("Dropping: " + Inventory.GetInstance ().itemBeingDragged);
		ResetIcon ();
	}

	public  void Drop(){
		/*Item temp = item;
		item = Inventory.GetInstance ().slotBeingDraggedFrom.item;
		Inventory.GetInstance ().slotBeingDraggedFrom.item = temp;*/
		int draggedFromIndex = -1;
		int draggedToIndex = -1;
		for (int i = 0; i < Inventory.GetInstance ().space; i++) {
			if (InventoryUI.GetInstance ().slots [i] == this) {
				draggedToIndex = i;
			}
		}
		for (int i = 0; i < Inventory.GetInstance ().space; i++) {
			if (InventoryUI.GetInstance ().slots [i] == Inventory.GetInstance().slotBeingDraggedFrom) {
				draggedFromIndex = i;
			}
		}
		Item temp = Inventory.GetInstance ().items [draggedFromIndex];
		Inventory.GetInstance ().items [draggedFromIndex] = Inventory.GetInstance ().items [draggedToIndex];
		Inventory.GetInstance ().items [draggedToIndex] = temp;

		Inventory.GetInstance ().onItemChangedCallback.Invoke ();
	}
}
