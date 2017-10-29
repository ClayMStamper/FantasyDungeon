using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


	public float lookRadius = 10f;
	Transform target;
	NavMeshAgent agent;
	PlayerAttackController playerAttackController;
	CharacterCombat combat;

	void Start(){
		agent = GetComponent <NavMeshAgent> ();
		target = PlayerManager.GetInstance ().player.transform;
		playerAttackController = PlayerAttackController.GetInstance ();
		combat = GetComponent <CharacterCombat>();
	}

	void Update(){
		float distance = Vector3.Distance (target.transform.position, transform.position);

		if (distance <= lookRadius) {
			agent.SetDestination (target.position);

			if (distance <= agent.stoppingDistance) {
				FaceTarget ();
				CharacterStats targetsStats = target.GetComponent <CharacterStats> ();
				if (targetsStats != null) {
					combat.Attack (targetsStats);
				}
			}
			if (!playerAttackController.enemiesInFieldOfAttack.Contains (transform)) {
				playerAttackController.AddToFieldOfAttack (transform);
			}
		} else {
			if (playerAttackController.enemiesInFieldOfAttack.Contains (transform)) {
				playerAttackController.RemoveFromFieldOfAttack (transform);
			}
		}
	}

	void FaceTarget(){
		transform.LookAt (target, transform.position + new Vector3 (0, 0, 1));

		Vector3 eulerAngles = transform.rotation.eulerAngles;
		eulerAngles.x = 0;
		eulerAngles.z = 0;

		transform.rotation = Quaternion.Euler (eulerAngles);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, transform.localScale);
	}

}
