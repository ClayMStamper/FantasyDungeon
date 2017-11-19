using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public List <Item> items = new List <Item>();

	public int space = 28;
	public Item itemBeingDragged;

	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallback;

	#region Singelton
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

	public bool Add (Item item){
		
		if (items.Count >= space) {
			print ("INVENTORY FULL");
			return false;
		}
	//	print ("Adding " + item + " to inventory");
		items.Add (item);
		
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke ();
		
		return true;
	}

	public void Remove (Item item){
		items.Remove (item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke ();
	}
}
