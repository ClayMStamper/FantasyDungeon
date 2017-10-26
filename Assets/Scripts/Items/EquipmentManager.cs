using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	public Equipment[] currentEquipment;

	Inventory inventory;

	public delegate void OnEquipmentChanged ();
	public OnEquipmentChanged onEquipmentChangedCallback;

	#region Singelton
	private static EquipmentManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public static EquipmentManager GetInstance(){
		return instance;
	}
	#endregion

	void Start(){
		// string array of all types of equipment
		inventory = Inventory.GetInstance();
		int numSlots = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
	//	currentMeshes = new SkinnedMeshRenderer[numSlots];
	}

	public void Equip(Equipment newItem){
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = Unequip (slotIndex);

	//	if (onEquipmentChanged != null) {
	//		onEquipmentChanged.Invoke (newItem, oldItem);
	//	}

		currentEquipment [slotIndex] = newItem;
		onEquipmentChangedCallback.Invoke ();
		inventory.onItemChangedCallback.Invoke ();
//		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer> (newItem.mesh);
	//	newMesh.transform.SetParent (targetMesh.transform);

		//newMesh.bones = targetMesh.bones;
	//	newMesh.rootBone = targetMesh.rootBone;
		//currentMeshes [slotIndex] = newMesh;
	}

	public Equipment Unequip(int slotIndex){
		if (currentEquipment [slotIndex] != null) {
		//	if (currentMeshes [slotIndex] != null) {
	//			Destroy (currentMeshes [slotIndex].gameObject);
	//		}
			Equipment oldItem = currentEquipment [slotIndex];
			inventory.Add (oldItem);

			//if (onEquipmentChanged != null) {
				//onEquipmentChanged.Invoke (null, oldItem);
		//	}

			currentEquipment [slotIndex] = null;
			onEquipmentChangedCallback.Invoke ();
			inventory.onItemChangedCallback.Invoke ();
			return oldItem;
		}
		return null;
	}

	public void UnequipAll(){
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.U)) {
			UnequipAll ();
		}
	}

}
