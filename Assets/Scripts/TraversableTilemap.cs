using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TraversableTilemap : ITraversableTilemap
{
	private Tilemap m_Tilemap;

	public TraversableTilemap(Tilemap tilemap)
	{
		m_Tilemap = tilemap;
	}

	public IEnumerable<Vector2Int> GetNeighbours(Vector2Int position)
	{
		var directions = (Direction[])Enum.GetValues(typeof(Direction));
		return directions.Select(x => position + x.ToVector2Int()).Where(IsWalkable);
	}

	private bool IsWalkable(Vector2Int position)
	{
		var tile = m_Tilemap.GetTile<MapTile>((Vector3Int)position);
		return tile == null || tile.Walkable;
	}
}
