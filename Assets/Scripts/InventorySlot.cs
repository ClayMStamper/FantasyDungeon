using UnityEngine;
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

	void ResetIcon(){
		icon.transform.localPosition = Vector3.zero;
		icon.transform.localScale = new Vector3 (1f, 1f, 1f);
		icon.GetComponent <Canvas> ().sortingOrder = 1;
	}

	public void Drag(){
		Inventory.GetInstance ().itemBeingDragged = item;
		icon.GetComponent <Canvas> ().sortingOrder = 2;
		icon.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
		icon.transform.position = Input.mousePosition;
	}

	public void EndDrag(){
		print ("Dropping: " + Inventory.GetInstance ().itemBeingDragged);
		ResetIcon ();
	}
}
