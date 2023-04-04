using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Tooltip("How long it takes to turn the player")]
    public float TurnDuration = 0.25f;
    [Tooltip("How long it takes to move the player")]
    public float MoveDuration = 0.2f;
	public bool NoClip;

	[SerializeField]
	private Tilemap m_Tilemap;
	private Vector2Int m_TilePosition;
	private Direction m_Direction;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveForward();
		}

		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveBackward();
		}

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			TurnLeft();
		}

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			TurnRight();
		}
	}

	public void TurnLeft()
	{
		Turn(m_Direction.NextCounterClockwise());
	}

	public void TurnRight()
	{
		Turn(m_Direction.NextClockwise());
	}

	public void Turn(Direction newDirection)
	{
		CancelAnimations();

        m_Direction = newDirection;

		LeanTween.rotateY(gameObject, newDirection.ToRotation(), TurnDuration).setEaseOutExpo();
	}

	public void MoveForward()
	{
		Move(m_TilePosition + m_Direction.ToVector2Int());
	}

	public void MoveBackward()
	{
		Move(m_TilePosition - m_Direction.ToVector2Int());
	}

	public bool IsTileWalkable(Vector2Int position)
	{
		if (NoClip)
		{
			return true;
		}

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

	public void Move(Vector2Int newPosition)
	{
		CancelAnimations();

		if (!IsTileWalkable(newPosition))
		{
			return;
		}

		m_TilePosition = newPosition;
		var worldPosition = m_Tilemap.GetCellCenterWorld((Vector3Int)m_TilePosition);

		LeanTween.move(gameObject, worldPosition, MoveDuration).setEaseOutExpo();
	}

	private void CancelAnimations()
	{
		LeanTween.cancel(gameObject);

		transform.position = m_Tilemap.GetCellCenterWorld((Vector3Int)m_TilePosition);
		transform.eulerAngles = new Vector3(0, m_Direction.ToRotation(), 0);
	}
}
