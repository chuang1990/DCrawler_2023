using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMovement))]
public class PlayerController : MonoBehaviour
{
	public float TurnDuration = 0.25f;

	public IInteractable Interactable => m_Interactable;

	private IInteractable m_Interactable;
	private Camera m_Camera;
	private Direction m_Direction;
	private TileMovement m_TileMovement;

	public void Battle(EnemyController enemy)
	{
		GameEvents.Instance.OnBattleInitiated?.Invoke(enemy);
	}

	private void Awake()
	{
		m_TileMovement = GetComponent<TileMovement>();
		m_Camera = GetComponentInChildren<Camera>();
	}

	private void Start()
	{
		m_Direction = (Direction)(Mathf.FloorToInt(transform.eulerAngles.y / 90) % 4);

		SnapToDirection();
	}

	private void FixedUpdate()
	{
		var inFront = GetObjectInFront();

		if (inFront != null)
		{
			m_Interactable = inFront.GetComponent<IInteractable>();
		}
		else
		{
			m_Interactable = null;
		}
	}

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

		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
		{
			Interact();
		}
	}

	private void Interact()
	{
		if (m_Interactable != null)
		{
			m_Interactable.Interact(gameObject);
		}
	}

	private void TurnLeft()
	{
		Turn(m_Direction.NextCounterClockwise());
	}

	private void TurnRight()
	{
		Turn(m_Direction.NextClockwise());
	}

	private void Turn(Direction newDirection)
	{
		SnapToDirection();

        m_Direction = newDirection;

		LeanTween.rotateY(m_Camera.gameObject, newDirection.ToRotation(), TurnDuration).setEaseOutExpo();
	}

	private void MoveForward()
	{
		m_TileMovement.Move(m_Direction);
	}

	private void MoveBackward()
	{
		m_TileMovement.Move(m_Direction.Opposite());
	}

	private void SnapToDirection()
	{
		LeanTween.cancel(m_Camera.gameObject);

		m_Camera.transform.eulerAngles = new Vector3(0, m_Direction.ToRotation(), 0);
	}

	/// <summary>
	/// Returns the game object in front of the camera via a physics ray cast
	/// </summary>
	/// <returns>Game object in front of camera</returns>
	private GameObject GetObjectInFront()
	{
		var layerMask = LayerMask.GetMask("Default", "Wall");
		var maxDistance = 2 * m_TileMovement.TileSize;
		var heightOffset = 0.5f;

		if (Physics.Raycast(m_Camera.transform.position + new Vector3(0, heightOffset, 0), m_Camera.transform.forward, out var hit, maxDistance, layerMask))
		{
			return hit.collider.gameObject;
		}

		return null;
	}
}
