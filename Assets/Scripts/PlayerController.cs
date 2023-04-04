using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TileMovement))]
public class PlayerController : MonoBehaviour
{
	public float TurnDuration = 0.25f;

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
		CancelAnimations();

		m_TileMovement.OnMoved.AddListener(CheckForInteractions);
	}
	
	private void CheckForInteractions()
	{
		var entities = FindObjectsOfType<TileMovement>();

		var interactable = entities.Where(x => x.Position == m_TileMovement.Position)
			.Select(x => x.GetComponent<IInteractable>())
			.Where(x => x != null)
			.FirstOrDefault();

		interactable?.Interact(gameObject);
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
		CancelAnimations();

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

	private void CancelAnimations()
	{
		LeanTween.cancel(m_Camera.gameObject);

		m_Camera.transform.eulerAngles = new Vector3(0, m_Direction.ToRotation(), 0);
	}
}
