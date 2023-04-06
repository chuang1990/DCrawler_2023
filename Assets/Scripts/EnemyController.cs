using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMovement))]
public class EnemyController : MonoBehaviour
{
	public int PursueDistance = 3;

	private TileMovement m_TileMovement;
	private Vector2Int m_StartingPosition;
	private TileMovement m_Player;
	private PathFinder m_PathFinder;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.SendMessage("Battle", this);
		}
	}

	public void Step()
	{
		if (Distance(m_Player.Position, m_TileMovement.Position) <= PursueDistance)
		{
			MoveTowards(m_Player.Position);
		}
		else
		{
			MoveTowards(m_StartingPosition);
		}
	}

	private void MoveTowards(Vector2Int position)
	{
		if (m_TileMovement.Position == position)
		{
			return;
		}
		
		var path = m_PathFinder.FindPath(m_TileMovement.Position, position);

		if (path.Length > 1)
		{
			m_TileMovement.Move(path[1]);
		}
	}

	private static int Distance(Vector2Int from, Vector2Int to)
	{
		return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
	}

	//private bool CanSee(GameObject gameObject)
	//{
	//	var direction = (gameObject.transform.position - transform.position).normalized;
	//	var distance = Vector3.Distance(gameObject.transform.position, transform.position);

	//	return !Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, distance, LayerMask.GetMask("Wall"));
	//}

	private void Awake()
	{
		m_TileMovement = GetComponent<TileMovement>();
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<TileMovement>();

		var tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
		m_PathFinder = new PathFinder(new TraversableTilemap(tilemap));
	}

	private void Start()
	{
		m_StartingPosition = m_TileMovement.Position;
	}
}
