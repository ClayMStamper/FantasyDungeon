using UnityEngine;
using System.Collections;

public class GroundGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public int mapSize;


	void Start(){
		//GenerateMap ();
	}

	[ContextMenu("GenerateMap")]
	public void GenerateMap(){

		string holderName = "Generated Ground";
		if (transform.Find (holderName)) {
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int x = -4; x < mapSize-4; x++) {
			Vector3 tilePosition = new Vector3 (x  * (tilePrefab.localScale.x - .5f), transform.position.y, 1);
			Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.identity) as Transform;
			newTile.SetParent (mapHolder);
		}
	}
}
