using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour {

	Inventory inventory;
	public Transform CharacterSlotsParent;
	public GameObject characterUI;
	static CharacterUI instance;

	public EquippedSlot[] slots;

	public static CharacterUI GetInstance(){
		return instance;
	}

	void Start () {
		instance = this;
		EquipmentManager.GetInstance ().onEquipmentChangedCallback += UpdateUI;

		//inventory = Inventory.GetInstance ();

		slots = CharacterSlotsParent.GetComponentsInChildren <EquippedSlot> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			characterUI.SetActive (!characterUI.activeSelf);
		}
	}

	void UpdateUI(){
		for (int i = 0; i < slots.Length; i++) {
			for (int j = 0; j < EquipmentManager.GetInstance ().currentEquipment.Length; j++) {
				if (EquipmentManager.GetInstance().currentEquipment[j] != null){
					if ((int)slots [i].slotType == (int)EquipmentManager.GetInstance ().currentEquipment [j].equipSlot) {
						slots [i].Equip (EquipmentManager.GetInstance ().currentEquipment [j]);
					}
				}
			}
		}
	}
}