using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTextUI : MonoBehaviour {

	public MyStat myStat;
	public PlayerStats playerStats;
	Text myText;

	void Start () {
		myText = GetComponent <Text> ();
		switch (myStat) {
		case MyStat.health:
			StartCoroutine ("UpdateHealthText");
			break;
		case MyStat.armor:
			StartCoroutine ("UpdateArmorText");
			break;
		case MyStat.damage:
			StartCoroutine ("UpdateDamageText");
			break;
		default:
			return;
		}
	}

	IEnumerator UpdateArmorText(){
		while (true) {
			myText.text = "Armor: " + playerStats.armor.GetValue ();
			yield return null;
			print ("updating");
		}
	}
	IEnumerator UpdateHealthText(){
		while (true) {
			myText.text = "Health: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
			yield return null;
		}
	}
	IEnumerator UpdateDamageText(){
		while (true) {
			myText.text = "Damage: " + playerStats.damage.GetValue ();
			yield return null;
		}
	}
}


public enum MyStat{health, armor, damage}