using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


	public float lookRadius = 10f;
	Transform target;
	NavMeshAgent agent;

	void Start(){
		agent = GetComponent <NavMeshAgent> ();
		target = PlayerManager.GetInstance ().player.transform;
	}

	void Update(){
		float distance = Vector3.Distance (target.transform.position, transform.position);

		if (distance <= lookRadius) {
			agent.SetDestination (target.position);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}

}
