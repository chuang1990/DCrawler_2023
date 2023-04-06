using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoordinator : MonoBehaviour
{
	public float StepInterval = 1;
	public float StartDelay = 2;

	private void Start()
	{
		InvokeRepeating(nameof(Step), StartDelay, StepInterval);
	}

	private void Step()
	{
		if (!enabled)
		{
			return;
		}

		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			enemy.SendMessage("Step", SendMessageOptions.DontRequireReceiver);
		}
	}
}
