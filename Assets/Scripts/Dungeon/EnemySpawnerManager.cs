using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

	float maxEnemyLevel = 10f;
	[Tooltip ("X coord range to allow spawnable enemy levels")]
	public float spawnRate = 10f;
	public MapGenerator mapGenerator;
	public GameObject[] enemyPrefabs;
	Transform enemiesHolder;

	#region Singelton
	private static EnemySpawnerManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public static EnemySpawnerManager GetInstance(){
		return instance;
	}
	#endregion

	void Start(){

	}

	[ContextMenu("Spawn all enemies")]
	void SpawnAllEnemies(){
		string holderName = ("Generated Enemies");

		if (transform.Find (holderName)){
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		enemiesHolder = new GameObject (holderName).transform;
		enemiesHolder.SetParent (transform);
		for (int x = (int)(- mapGenerator.mapSize.x / 2); x < mapGenerator.mapSize.x / 2; x++) {
			for (int y = (int)mapGenerator.mapSize.y; y > -mapGenerator.mapSize.y / 2; y--) {
				Spawn (x, y);
			}
		}
	}

	public void Spawn(int x, int y){
		if (Random.Range (-mapGenerator.mapSize.y / 2, mapGenerator.mapSize.y / 2) >= y) { //check to see if spawn (chances increase with depth in y)
			float xPercent = (x / mapGenerator.mapSize.x * 100f) + 50f;
			GameObject enemyToSpawn = PickEnemy (xPercent);
			if (enemyToSpawn != null) {
				GameObject newEnemy = Instantiate (enemyToSpawn);
				newEnemy.transform.position = new Vector3 (x, .5f, y);
				newEnemy.transform.SetParent (enemiesHolder);
			}
		}
	}

	GameObject PickEnemy(float xPercent){
		List <GameObject> spawnableEnemies = new List<GameObject> ();
		for (int i = 0; i < enemyPrefabs.Length; i++) {
			EnemyStats enemy = enemyPrefabs [i].GetComponent <EnemyStats> ();
			float enemyLevelPercent = (float)enemy.level.GetValue() / maxEnemyLevel * 100f;
			print ("xPercent : " + xPercent + "\nenemyLevelPercent : " + enemyLevelPercent);
			float difference = xPercent - enemyLevelPercent;
			if (difference <= spawnRate && xPercent - enemyLevelPercent >= -spawnRate) {
				spawnableEnemies.Add (enemyPrefabs [i]);
			}
		}
		try{
		return spawnableEnemies[Random.Range (-1, spawnableEnemies.Count)];
		} catch {
			return null;
		}
	}
}
