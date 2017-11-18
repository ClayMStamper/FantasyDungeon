using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region Singelton
	private static GameManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		DontDestroyOnLoad (instance);
	}

	public static GameManager GetInstance(){
		return instance;
	}
	#endregion
}
