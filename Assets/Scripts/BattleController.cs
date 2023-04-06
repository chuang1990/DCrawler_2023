using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
	public float MinStanceDuration = 2;
	public float MaxStanceDuration = 5;
	public float ChangeSideHealthPenalty = 2;
	public float SmashHeartMeterIncrement = 0.05f;

	[SerializeField]
	private BattleUI m_BattleUI;

	private Battle m_Battle;

	private void Start()
	{
		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);
	}

	private void OnBattleInitiated(EnemyController enemy)
	{
		m_Battle = new Battle();
		m_Battle.Enemy = enemy;
		m_BattleUI.Battle = m_Battle;
	}

	private void FinishBattle(BattleResult result)
	{
		Debug.Log($"Battle finished! Result was {result}");

		Destroy(m_Battle.Enemy.gameObject);

		GameEvents.Instance.OnBattleFinished?.Invoke(m_Battle.Enemy, result);

		m_Battle = null;
	}

	private void Update()
	{
		if (m_Battle == null)
		{
			return;
		}

		if (Time.time > m_Battle.ChangeStanceTime)
		{
			ChangeStance();
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			SmashButton(Button.A);
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			SmashButton(Button.B);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			SmashButton(Button.C);
		}
	}

	private void ChangeSide(Side side)
	{
		if (m_Battle.Side == side)
		{
			return;
		}

		if (m_Battle.Side != Side.Neutral)
		{
			GiveHealthPenalty();
		}

		m_Battle.Side = side;

		m_Battle.OnSideChanged();
	}

	private void SmashButton(Button button)
	{
		m_Battle.OnButtonSmashed(button);

		if (button == m_Battle.Stance)
		{
			m_Battle.HeartMeter += SmashHeartMeterIncrement;
			ChangeSide(Side.FullHeart);
		}
		else
		{
			m_Battle.HeartMeter -= SmashHeartMeterIncrement;
			ChangeSide(Side.BrokenHeart);
		}

		if (m_Battle.HeartMeter < -1)
		{
			FinishBattle(BattleResult.BrokenHeart);
		}
		else if (m_Battle.HeartMeter > 1)
		{
			FinishBattle(BattleResult.FullHeart);
		}
	}

	private void GiveHealthPenalty()
	{
		Debug.Log("Health penalty!");
	}

	private void ChangeStance()
	{
		m_Battle.ChangeStanceTime = Time.time + Random.Range(MinStanceDuration, MaxStanceDuration);

		var buttons = (Button[])System.Enum.GetValues(typeof(Button));
		m_Battle.Stance = buttons[Random.Range(0, buttons.Length)];

		m_Battle.OnStanceChanged();
	}
}
