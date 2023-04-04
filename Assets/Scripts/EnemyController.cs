using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMovement))]
public class EnemyController : MonoBehaviour, IInteractable
{
	public int PursueDistance = 3;

	private TileMovement m_TileMovement;
	private Tilemap m_Tilemap;
	private Vector2Int m_StartingPosition;

	public void Interact(GameObject interactor)
	{
		interactor.SendMessage("Battle", this);
	}

	public void Step()
	{
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<TileMovement>();

		var pathFinder = new PathFinder(new TraversableTilemap(m_Tilemap));
		var path = pathFinder.FindPath(m_TileMovement.Position, player.Position);

		if (path.Length > 0)
		{
			m_TileMovement.Move(path[1]);
		}

		//var positionDelta = player.Position - m_TileMovement.Position;
		//var distance = Mathf.Abs(positionDelta.x) + Mathf.Abs(positionDelta.y);

		//if (distance <= PursueDistance && CanSee(player.gameObject))
		//{
		//	m_TileMovement.MoveTo(player.Position);
		//}
	}

	private bool CanSee(GameObject gameObject)
	{
		var direction = (gameObject.transform.position - transform.position).normalized;
		var distance = Vector3.Distance(gameObject.transform.position, transform.position);

		return !Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, distance, LayerMask.GetMask("Wall"));
	}

	private void Awake()
	{
		m_TileMovement = GetComponent<TileMovement>();
		m_Tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
	}

	private void Start()
	{
		m_StartingPosition = m_TileMovement.Position;
	}
}
