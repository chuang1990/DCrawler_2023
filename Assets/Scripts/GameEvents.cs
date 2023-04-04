using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
	public static GameEvents Instance => s_Instance;

    public UnityEvent<EnemyController> OnBattleInitiated;
	public UnityEvent<EnemyController> OnBattleFinished;

	private static GameEvents s_Instance;

	private void Awake()
	{
		if (s_Instance == null)
		{
			s_Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
