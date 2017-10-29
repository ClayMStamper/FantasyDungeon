using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	private float attackCooldown = 0f;
	CharacterStats myStats;

	void Start(){
		myStats = GetComponent <CharacterStats> ();
		//attackSpeed = myStats.attackSpeed.GetValue ();

	}

	void Update(){
		attackCooldown -= Time.deltaTime;
		//attackSpeed = myStats.attackSpeed.GetValue ();
		//print (myStats.attackSpeed.GetValue ());
	}

	public void Attack(CharacterStats targetsStats){
		if (attackCooldown <= 0f) {
			targetsStats.TakeDamage (myStats.damage.GetValue ());
			attackCooldown = 1f / myStats.attackSpeed.GetValue ();;
		}
	}
}
