using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	void Start () {
		EquipmentManager.GetInstance ().onEquipmentChangedCallback += OnEquipmentChanged;
	}

	void OnEquipmentChanged(Equipment newItem, Equipment oldItem){
		if (newItem != null) {
			armor.AddModifier (newItem.armorModifier);
			damage.AddModifier (newItem.damageModifier);
			attackSpeed.AddModifier (newItem.attackSpeedModifer);
			attackRange.AddModifier (newItem.attackRangeModifier);
		}

		if (oldItem != null) {
			armor.RemoveModifier (oldItem.armorModifier);
			damage.RemoveModifier (oldItem.damageModifier);
			attackSpeed.RemoveModifier (oldItem.attackSpeedModifer);
			attackRange.RemoveModifier (oldItem.attackRangeModifier);
		}
	}

}
