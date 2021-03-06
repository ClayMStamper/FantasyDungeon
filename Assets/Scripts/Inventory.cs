﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public List <Item> items = new List <Item>(24);

	public int space = 24;

	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallback;

	#region Singleton
	private static Inventory instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
			
	}

	public static Inventory GetInstance(){
		return instance;
	}
	#endregion

	void Start(){

		for (int i = 0; i < space; i++) {
			items.Add (null);
		}
	}

	public bool Add (Item item){
		
	/*	if (items.Count >= space) {
			print ("INVENTORY FULL");
			return false;
		}*/
	//	print ("Adding " + item + " to inventory");
		//items.Add (item);

		for (int i = 0; i < space; i++) {
			if (items [i] == null) {
				items [i] = item;
				break;
			} else if (i >= space){
				print ("INVENTORY FULL");
				return false;
			}
		}

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke ();
		
		return true;
	}

	//probably causing glitches: change to take in inventory slot instead of item
	public void Remove (Item item){
		for (int i = 0; i < space; i++) {
			if (items [i] == item) {
				items [i] = null;
				break;
			}

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
	}
}