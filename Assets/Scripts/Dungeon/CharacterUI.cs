using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour {

	Inventory inventory;
	public Transform CharacterSlotsParent;
	public GameObject characterUI;
	static CharacterUI instance;
	EquipmentManager equipmentManager;

	public EquippedSlot[] slots;

	public static CharacterUI GetInstance(){
		return instance;
	}

	void Start () {
		instance = this;
		equipmentManager = EquipmentManager.GetInstance ();
		equipmentManager.onEquipmentChangedCallback += UpdateUI;

		//inventory = Inventory.GetInstance ();

		slots = CharacterSlotsParent.GetComponentsInChildren <EquippedSlot> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			characterUI.SetActive (!characterUI.activeSelf);
		}
	}

	void UpdateUI(Equipment fillerParam, Equipment fillerParam2){
		for (int i = 0; i < slots.Length; i++) {
			for (int j = 0; j < equipmentManager.currentEquipment.Length; j++) {
				if (equipmentManager.currentEquipment [j] != null) {
					if ((int)slots [i].slotType == (int)equipmentManager.currentEquipment [j].equipSlot) {
						slots [i].Equip (equipmentManager.currentEquipment [j]);
					}
				} else {
					slots [j].ClearSlot ();
				}
			}
		}
	}
}