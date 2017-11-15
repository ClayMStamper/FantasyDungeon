using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

	#region Singelton
	private static PlayerAttackController instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public static PlayerAttackController GetInstance(){
		return instance;
	}
	#endregion

	Animator anim;
	CharacterStats playerStats;
	public SphereCollider fieldOfAttack;
	CharacterCombat combat;
	private float attackCooldown = 0f;
	public List <Transform> enemiesInFieldOfAttack = new List <Transform>();
	float attackRange;

	void Start(){
	//	anim = GetComponent <Animator> ();
		playerStats = GetComponent <CharacterStats> ();
		combat = GetComponent <CharacterCombat> ();

	}

	void Update(){
		attackCooldown -= Time.deltaTime;
		if (Input.GetKey (KeyCode.Space)){
			Attack ();
		}
	}

	public void Attack(){
//		print ("Attacking");
		if (attackCooldown <= 0f) {
			attackRange = playerStats.attackRange.GetValue () / 10;
			foreach (Transform enemy in enemiesInFieldOfAttack) {
				print (enemy.name);
				if (Facing (enemy)) {
					//check for in range
					if (Vector3.Distance (transform.position, enemy.position) < (attackRange)) {
						enemy.GetComponent <CharacterStats> ().TakeDamage (playerStats.damage.GetValue ());
						attackCooldown = 1f / playerStats.attackSpeed.GetValue ();
					}
				}
			}
		}
	}

	public void AddToFieldOfAttack(Transform newEnemy){
		enemiesInFieldOfAttack.Add (newEnemy);
	}

	public void RemoveFromFieldOfAttack(Transform oldEnemy){
		enemiesInFieldOfAttack.Remove (oldEnemy);
	}

	bool Facing(Transform target){
		Vector3 direction = (target.position - transform.position).normalized;
		float dot = Vector3.Dot (direction, transform.forward);

		if (dot > 0.9) {
			return true;
		} else {
			return false;
		}
	}
		
}
