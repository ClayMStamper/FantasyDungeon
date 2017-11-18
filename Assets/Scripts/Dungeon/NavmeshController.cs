using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour {

	MapGenerator mapGenerator;
	NavMeshSurface surface;

	[ContextMenu ("Bake")]
	void bakeMesh(){
		surface = GetComponent <NavMeshSurface> ();
		mapGenerator = transform.parent.GetComponent <MapGenerator> ();
		transform.localScale = new Vector3 (mapGenerator.mapSize.x, mapGenerator.mapSize.y, 1f);
		surface.size = transform.localScale;
		surface.BuildNavMesh ();
	}

}
