using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeUI : MonoBehaviour {

	Inventory inventory;
	public Transform componentSlotsParent;
	public GameObject forgeUI;
	public ForgeUITrigger forgeUITrigger;

	public ComponentSlot[] slots;

	#region Singelton
	private static ForgeUI instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

	}

	public static ForgeUI GetInstance(){
		return instance;
	}
	#endregion

	void Start () {
		forgeUITrigger.forgeUI = this;
		inventory = Inventory.GetInstance ();
		inventory.onItemChangedCallback += UpdateForgeUI;
		//		inventory.onItemChangedCallback += LateUpdateUI;

		slots = componentSlotsParent.GetComponentsInChildren <ComponentSlot> ();
		forgeUI.SetActive (false);
	}

	public void UpdateForgeUI(){
		print ("updating UI");
		for (int i = 0; i < slots.Length; i++) {
			if (inventory.items [i] != null) {
				slots [i].AddItemUI (inventory.items [i]);
			} else {
				slots [i].ClearSlotUI ();
			}
		}
		foreach (ComponentSlot slot in slots) {
			if (slot.item != null) {
				slot.icon.sprite = slot.item.icon;
			} else {
				slot.icon.sprite = null;
			}
		}
	}

	public void SetActiveTrue(){
		forgeUI.SetActive(true);
	}

	public void SetActiveFalse(){
		forgeUI.SetActive(false);
	}
}
