using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	#region Singelton
	private static PlayerManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public static PlayerManager GetInstance(){
		return instance;
	}
	#endregion

	public GameObject player;

	void Start(){
		FindPlayer ();
		SetPlayerPos ();
		LevelManager.GetInstance().onLevelWasLoadedCallback += FindPlayer;
		LevelManager.GetInstance().onLevelWasLoadedCallback += SetPlayerPos;
	}

	void SetPlayerPos(){
		player.transform.position = transform.position;
	}

	void FindPlayer(){
		if (player == null){
			player = FindObjectOfType <PlayerMotor>().gameObject;
		}
	}
}
