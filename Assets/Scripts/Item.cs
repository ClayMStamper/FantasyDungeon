﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	public Component[] myComponents;

	new public string name = "New Item";
	public Sprite icon = null;
	[Range(0,100)]
	public float dropChance;

	public virtual void Use(){
		// use item
		// something might happen

		Debug.Log ("Using " + name);
	}
	public virtual void Use(int inventorySlotIndex){
		// use item
		// something might happen

		Debug.Log ("Using " + name);
	}

	public void RemoveFromInventory(){
		Inventory.GetInstance ().Remove (this);
	}
}

public enum Component {stick, cloth, bone, rock, iron, steel, gold, leather, stardust}
