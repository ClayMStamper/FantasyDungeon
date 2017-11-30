using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public delegate void OnLevelLevelWasLoaded ();
	public OnLevelLevelWasLoaded onLevelWasLoadedCallback;

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

	void OnEnable(){
		SceneManager.sceneLoaded += LevelFinishedLoading;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= LevelFinishedLoading;
	}

	void LevelFinishedLoading (Scene scene, LoadSceneMode mode){
		if (onLevelWasLoadedCallback != null) {
			onLevelWasLoadedCallback.Invoke ();
		}
	//	Debug.Break ();
	}

	public void LoadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

}
