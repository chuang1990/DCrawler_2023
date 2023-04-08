using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController m_Player;
	[SerializeField]
	private EnemyCoordinator m_EnemyCoordinator;

	private void Start()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);
		GameEvents.Instance.OnBattleFinished.AddListener(OnBattleFinished);

		m_Player.GetComponent<Health>().Died.AddListener(OnPlayerDied);
	}

	private void OnPlayerDied()
	{
		Debug.Log("Player died!");
	}

    public void OnBattleInitiated(EnemyController enemy)
    {
		m_Player.enabled = false;
		m_EnemyCoordinator.enabled = false;
	}

	public void OnBattleFinished(EnemyController enemy, BattleResult result)
	{
		m_Player.enabled = true;
		m_EnemyCoordinator.enabled = true;
	}
}
