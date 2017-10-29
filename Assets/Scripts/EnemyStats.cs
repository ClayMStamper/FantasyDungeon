using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

	public override void Die ()
	{
		base.Die ();

		PlayerAttackController.GetInstance ().RemoveFromFieldOfAttack (transform);

		GetComponent <Enemy> ().DropLoot ();

		Destroy (gameObject);

	}
}
