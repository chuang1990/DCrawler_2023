using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
	[SerializeField]
	private BattleUI m_BattleUI;

	private EnemyController m_Enemy;

	private void Start()
	{
		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);

		m_BattleUI.OnRan.AddListener(OnRan);
	}

	private void OnBattleInitiated(EnemyController enemy)
	{
		m_Enemy = enemy;
	}

	private void OnRan()
	{
		Destroy(m_Enemy.gameObject);

		GameEvents.Instance.OnBattleFinished?.Invoke(m_Enemy);
	}
}
