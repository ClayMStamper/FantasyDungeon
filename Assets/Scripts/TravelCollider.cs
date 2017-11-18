using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelCollider : MonoBehaviour {

	Vector2 mapSize;
	BoxCollider myCollider;

	void Start(){
		mapSize = MapGenerator.GetInstance ().mapSize;
		transform.position = new Vector3 (0f, 0f, mapSize.y / 2);
		myCollider = gameObject.AddComponent <BoxCollider>();
		myCollider.size = new Vector3 (mapSize.x, 5f, 1f);
	}

	void OnTriggerEnter(Collider col){
		LevelManager.GetInstance().LoadLevel ("01a_Surface");
	}
}
