using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour {

	public LevelManager levelManager;

	void OnTriggerEnter2D(Collider2D col){
		levelManager.LoadLevel ("01b_Dungeon");
	}
}
