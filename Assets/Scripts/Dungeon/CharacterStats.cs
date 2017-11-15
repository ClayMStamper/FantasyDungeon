using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public int maxHealth = 100;
	public int currentHealth { get; private set;}

	public Stat damage;
	public Stat armor;
	public Stat attackSpeed;
	[Tooltip ("Multiply disired worldview distance by 10")]
	public Stat attackRange;

	void Awake(){
		currentHealth = maxHealth;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			TakeDamage (5);
		}
	}

	public virtual void TakeDamage(int damageTaken){

		currentHealth -= DamageTakenAfterArmor(damageTaken);
		Debug.Log (transform.name + " took " + damageTaken + " damage!\n" + DamageTakenAfterArmor (damageTaken));
		Debug.Log ( "\nHealth = " + currentHealth);

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public virtual void Die(){
		Debug.Log (transform.name.ToUpper() + " DIED!");
	}

	int DamageTakenAfterArmor(int rawDamageTaken){
		return  Mathf.Clamp (rawDamageTaken - armor.GetValue(), 0, int.MaxValue);
	}
}
