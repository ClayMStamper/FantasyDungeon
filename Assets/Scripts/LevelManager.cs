using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	#region Singelton
	private static LevelManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

		DontDestroyOnLoad (instance);

	}

	public static LevelManager GetInstance(){
		return instance;
	}
	#endregion

	public void LoadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

}
