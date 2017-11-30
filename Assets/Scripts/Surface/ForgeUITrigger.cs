using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeUITrigger : MonoBehaviour {

	public ForgeUI forgeUI;
	public GameObject inventoryUI, characterUI;

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			forgeUI.SetActiveTrue ();
			inventoryUI.SetActive (false);
			characterUI.SetActive (false);
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			forgeUI.SetActiveFalse ();
		}
	}
}
