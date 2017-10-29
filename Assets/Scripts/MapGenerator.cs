using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public Transform obstaclePrefab;
	public Vector3 mapHolderOffset;
	public Vector2 mapSize;
	Coord spawnTile;

	[Range(0,1)]
	public float obstaclePercent;

	public int seed = 10;

	List <Coord> allTilleCoords;
	Queue <Coord> shuffledTileCoords;

	void Start(){
		//mapHolderOffset.x = (mapSize.x / 2
		transform.position = mapHolderOffset;
	}

	[ContextMenu("GenerateMap")]
	public void GenerateMap(){
		
		allTilleCoords = new List<Coord> (); // new list of Coords
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
				allTilleCoords.Add (new Coord (x, y));
			}
		}

		shuffledTileCoords = new Queue<Coord> (Utility.ShuffleArray (allTilleCoords.ToArray (), seed));
		spawnTile  = new Coord((int)mapSize.x / 2,(int)mapSize.y / 2);

		// make tile parent and instantate tiles
		string holderName = ("Generated Map");

		if (transform.Find (holderName)){
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.SetParent (transform);
		//mapHolder.localPosition = mapHolderOffset;

		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++){
				Vector3 tilePosition = CoordToPosition (x, y);
				Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.identity);
				newTile.SetParent (mapHolder);
			}
		}

		bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y]; 

		int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
		int currentObstacleCount = 0;
	
		for (int i = 0; i < obstacleCount; i++) {
			
			Coord randomCoord = GetRandomCoord ();
			obstacleMap [randomCoord.x, randomCoord.y] = true;
			currentObstacleCount++;

			if (randomCoord != spawnTile && MapIsFullyAccessible (obstacleMap, currentObstacleCount)) {
				Vector3 obstaclePos = CoordToPosition (randomCoord.x, randomCoord.y);

				Transform newObstacle = Instantiate (obstaclePrefab, obstaclePos, Quaternion.identity);
				newObstacle.SetParent (mapHolder);
			} else {
				obstacleMap [randomCoord.x, randomCoord.y] = false;
				currentObstacleCount--;
			}
		}
	}
	/*
	public bool MapIsFullyAccessible(bool[,] visitedObstacles, int obstacleCount) {
		// Boolean arrays are initialized with false
		bool[,] visitedNeighbors = new bool[(int)mapSize.x, (int)mapSize.y];
		Queue<Coord> queue = new Queue<Coord>();
		int accessibleTileCount = 1;
		int walkableTileCount = (int)(mapSize.x * mapSize.y - obstacleCount);

		Coord[] neighborOffsets = { 
			new Coord(-1, 0), // Left 
			new Coord(0, 1), // Top
			new Coord(1, 0), // Right
			new Coord(0, -1) // Bottom
		};

		queue.Enqueue(spawnTile);
		visitedNeighbors[(int)spawnTile.x, (int)spawnTile.y] = true;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();

			foreach (Coord neighborOffset in neighborOffsets) {
				Coord neighbor = tile + neighborOffset;

				// This condition checks that we stay within the bounds of our map.
				if ((neighbor.x >= 0 && neighbor.x < mapSize.x) && (neighbor.y >= 0 && neighbor.y <mapSize.y)) {
					if (visitedNeighbors[neighbor.x, neighbor.y] || visitedObstacles[neighbor.x, neighbor.y]) {
						continue;
					}

					visitedNeighbors[neighbor.x, neighbor.y] = true;
					queue.Enqueue(neighbor);
					accessibleTileCount += 1;
				}
			}
		}

		return walkableTileCount == accessibleTileCount;
	}
*/
	bool MapIsFullyAccessible (bool[,] obstacleMap, int currentObstacleCount){
		bool[,] mapFlags = new bool [obstacleMap.GetLength (0), obstacleMap.GetLength (1)];
		Queue <Coord> queue = new Queue<Coord> ();
		queue.Enqueue (spawnTile);
		mapFlags [spawnTile.x, spawnTile.y] = true;

		int accessibleTileCount = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();

			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					int NeighborX = tile.x + x;
					int NeighborY = tile.y + y;
					if (x == 0 || y == 0) {
						if (NeighborX >= 0 && NeighborX < obstacleMap.GetLength (0) && NeighborY >= 0 && NeighborY < obstacleMap.GetLength (1)) { // guaranteend its inside obstacle map
							if (!mapFlags [NeighborX, NeighborY] && !obstacleMap [NeighborX, NeighborY]) { // havnt checked yet and not an obstacle tile
								mapFlags [NeighborX, NeighborY] = true;
								queue.Enqueue (new Coord (NeighborX, NeighborY));
								//found accessible tile
								accessibleTileCount++;
							}
						}
					}
				}
			}
		}
		int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
		return targetAccessibleTileCount == accessibleTileCount;
	}

	Vector3 CoordToPosition(int x, int y){
		Vector3 newPos = new Vector3 ((float)(-mapSize.x / 2 + 0.5 + x), 0f, (float)( -mapSize.y / 2 + 0.5f + y));
		return newPos;
	}

	public Coord GetRandomCoord(){
		Coord randomCoord = shuffledTileCoords.Dequeue ();
		shuffledTileCoords.Enqueue (randomCoord);
		return randomCoord;
	}

	public struct Coord {
		public int x;
		public int y;

		public Coord(int _x, int _y){
			x = _x;
			y = _y;
		}

		public static bool operator ==(Coord c1, Coord c2){
			return c1.x == c2.x && c1.y == c2.y;	
		}

		public static bool operator !=(Coord c1, Coord c2){
			return !(c1 == c2);	
		}

		public static Coord operator +(Coord c1, Coord c2){
			return new Coord (c1.x + c2.x, c1.y + c2.y);	
		}

	}
}
