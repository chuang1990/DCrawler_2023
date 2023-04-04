using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileMovement))]
public class EnemyController : MonoBehaviour, IInteractable
{
	private TileMovement m_TileMovement;

	public void Interact(GameObject interactor)
	{
		interactor.SendMessage("Battle", this);
	}

	private void Awake()
	{
		m_TileMovement = GetComponent<TileMovement>();
	}
}
