using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController m_Player;

	private void Start()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);
		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleFinished);
	}

    public void OnBattleInitiated(EnemyController enemy)
    {
		m_Player.enabled = false;
    }

	public void OnBattleFinished(EnemyController enemy)
	{
		m_Player.enabled = true;
	}
}
