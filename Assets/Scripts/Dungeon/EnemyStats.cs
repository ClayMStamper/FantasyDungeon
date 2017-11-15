using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {

	public Stat level;
	public Canvas canvas;

	public override void TakeDamage (int damageTaken)
	{
		//DamageTakenIndicatorText (damageTaken);
		base.TakeDamage (damageTaken);
	}

	public override void Die ()
	{
		base.Die ();

		PlayerAttackController.GetInstance ().RemoveFromFieldOfAttack (transform);

		GetComponent <Enemy> ().CalculateAndDropLoot ();

		Destroy (gameObject);

	}

	void DamageTakenIndicatorText(int damageTaken){
		GameObject textObject = new GameObject ("Damage Indicator Text");
		textObject.transform.SetParent (canvas.transform);
		print ("HAPPENING");
		textObject.transform.position = transform.position;
		Text text = textObject.AddComponent <Text> ();
		text.text = "DSALFASL";
	}
}
