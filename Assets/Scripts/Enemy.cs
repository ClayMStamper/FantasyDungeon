using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

	PlayerManager playerManager;
	CharacterStats myStats;
	public float attackRange;
	[SerializeField]
	Item[] droppableLoot;

	void Start(){

		playerManager = PlayerManager.GetInstance ();
		myStats = GetComponent <CharacterStats> ();
		attackRange = ((float)myStats.attackRange.GetValue()) / 10;
		GetComponent <SphereCollider> ().radius = attackRange;
	}

	public override void Interact (Collider col)
	{
		base.Interact (col);
		CharacterCombat playerCombat = playerManager.GetComponent <CharacterCombat> ();
		if (playerCombat != null) {
			playerCombat.Attack (myStats);
		}

	}

	public void DropLoot(){
		GameObject drop = new GameObject();
		drop.transform.position = transform.position;
		drop.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		ItemPickup dropPickupComponent = drop.AddComponent <ItemPickup> ();
		dropPickupComponent.item = droppableLoot [Random.Range (0, droppableLoot.Length - 1)];
		SpriteRenderer dropSprite = drop.AddComponent <SpriteRenderer> ();
		BoxCollider dropdCollider = drop.AddComponent <BoxCollider> ();
		dropdCollider.size = new Vector3 (.3f, .36f, 1.33f);
	}
}
