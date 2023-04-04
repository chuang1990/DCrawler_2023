using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITraversableTilemap
{
	IEnumerable<Vector2Int> GetNeighbours(Vector2Int position);
}
