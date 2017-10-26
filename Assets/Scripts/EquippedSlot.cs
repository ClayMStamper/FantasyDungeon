using UnityEngine.UI;
using UnityEngine;

public class EquippedSlot : MonoBehaviour {

	public Equipment item;
	public Image icon;
	public EquipmentSlot slotType;
	public int currentEquipmentIndex;

	EquipmentManager equipmentManager;

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
			EquipmentManager.GetInstance ().Unequip (currentEquipmentIndex);
			ClearSlot ();
			//Inventory.GetInstance ().onItemChangedCallback.Invoke ();
		}
	}
}
