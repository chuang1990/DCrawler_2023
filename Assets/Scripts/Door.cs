using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public DoorColor DoorColor;

	private Tilemap m_Tilemap;
	private Vector3Int m_Position;

	private void Start()
	{
		m_Tilemap = GetComponentInParent<Tilemap>();
		m_Position = m_Tilemap.WorldToCell(transform.position);

		if (m_Tilemap.HasTile(m_Position + (Vector3Int)Direction.North.ToVector2Int()))
		{
			transform.localEulerAngles = new Vector3(0, 90, 0);
		}

		//var renderer = GetComponentInChildren<Renderer>();
		//renderer.material.color = DoorColor.Color;
	}

	public void Open()
    {
		m_Tilemap.SetTile(m_Position, null);
    }
}
