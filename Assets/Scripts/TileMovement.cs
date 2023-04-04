using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
	public UnityEvent OnMoved;
	public float MoveDuration = 0.2f;
	[HideInInspector]
	public Vector2Int Position;

	private Tilemap m_Tilemap;

	public bool Move(Direction direction)
	{
		return Move(Position + direction.ToVector2Int());
	}
	
	public bool Move(Vector2Int newPosition)
	{
		CancelAnimations();

		if (!IsTileWalkable(newPosition))
		{
			return false;
		}

		Position = newPosition;

		var worldPosition = m_Tilemap.GetCellCenterWorld((Vector3Int)Position);
		LeanTween.move(gameObject, worldPosition, MoveDuration).setEaseOutExpo();

		OnMoved?.Invoke();

		return true;
	}

	public bool IsTileWalkable(Vector2Int position)
	{
		if (!m_Tilemap.cellBounds.Contains((Vector3Int)position))
		{
			return false;
		}

		var tile = m_Tilemap.GetTile<MapTile>((Vector3Int)position);

		if (tile != null && !tile.Walkable)
		{
			return false;
		}

		return true;
	}

	public void CancelAnimations()
	{
		LeanTween.cancel(gameObject);

		transform.position = m_Tilemap.GetCellCenterWorld((Vector3Int)Position);
	}

	private void Awake()
	{
		m_Tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

		Position = (Vector2Int)m_Tilemap.WorldToCell(transform.position);

		CancelAnimations();
	}
}
