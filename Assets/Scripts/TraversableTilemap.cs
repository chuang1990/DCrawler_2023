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
		return new Vector2Int[]
		{
			new Vector2Int(position.x - 1, position.y),
			new Vector2Int(position.x + 1, position.y),
			new Vector2Int(position.x, position.y - 1),
			new Vector2Int(position.x, position.y + 1)
		}.Where(x =>
		{
			var tile = m_Tilemap.GetTile<MapTile>((Vector3Int)x);
			return tile == null || tile.Walkable;
		});
	}
}
