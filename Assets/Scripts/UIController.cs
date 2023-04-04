using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private GameObject m_BattleUI;

	private void Start()
	{
		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);
		GameEvents.Instance.OnBattleFinished.AddListener(OnBattleFinished);
	}

	public void OnBattleInitiated(EnemyController enemy)
	{
		m_BattleUI.gameObject.SetActive(true);
	}

	public void OnBattleFinished(EnemyController enemy)
	{
		m_BattleUI.gameObject.SetActive(false);
	}
}
