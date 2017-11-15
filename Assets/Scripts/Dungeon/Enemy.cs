using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

	PlayerManager playerManager;
	CharacterStats myStats;
	public Vector2 enemySpawnRange;
	public float attackRange;
	[SerializeField]
	Item[] droppableLoot;
	[Tooltip("Min and Max items that can be dropped by this enemy")]
	public Vector2 dropCountRange;

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

	public void CalculateAndDropLoot(){
		int dropCount = (int)Random.Range (dropCountRange.x, dropCountRange.y);
		List <Item> rolledToDrop = new List<Item>();

		for (int i = 0; i < droppableLoot.Length; i++) {
			if (Random.value * 100 < droppableLoot [i].dropChance) {
				rolledToDrop.Add (droppableLoot [i]);
			}
		}

		for (int i = 0; i < dropCount; i++) {
			int randomDropIndex = Random.Range (0, rolledToDrop.Count);
			if (rolledToDrop[randomDropIndex] != null) {
				DropLoot (rolledToDrop[randomDropIndex]);
				rolledToDrop.Remove (rolledToDrop [randomDropIndex]);
			}
		}
	}

	void DropLoot(Item item){
		GameObject drop = new GameObject();
		Vector3 offset = new Vector3 (Random.Range (-.3f, .3f), 0, Random.Range (-.3f, .3f));
		drop.transform.position = transform.position + offset;
		drop.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		SpriteRenderer dropSprite = drop.AddComponent <SpriteRenderer> ();
		ItemPickup dropPickupComponent = drop.AddComponent <ItemPickup> ();
		dropPickupComponent.item = item;
		BoxCollider dropCollider = drop.AddComponent <BoxCollider> ();
		dropCollider.size = new Vector3 (.3f, .36f, 1.33f);
	}
}
