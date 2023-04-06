using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public float RevealAnimationDuration = 0.75f;
	public float MaxCursorDisplacement = 120;
	[HideInInspector]
	public Battle Battle;

	[Header("Sprites")]
	[SerializeField]
	private Sprite m_JoanPlay;
	[SerializeField]
	private Sprite m_JoanNeutral;
	[SerializeField]
	private Sprite m_EnemySad;
	[SerializeField]
	private Sprite m_EnemyHappy;
	[Header("UI Elements")]
	[SerializeField]
	private Image m_HeartMeter;
	[SerializeField]
	private Image m_Cursor;
	[SerializeField]
	private Image m_Joan;
	[SerializeField]
	private Image m_Enemy;
	[SerializeField]
	private Image m_Stance;
	[SerializeField]
	private Image m_ControlA;
	[SerializeField]
	private Image m_ControlB;
	[SerializeField]
	private Image m_ControlC;

	private void OnEnable()
	{
		if (Battle == null)
		{
			return;
		}

		LeanTween.move(m_Joan.rectTransform, Vector3.zero, RevealAnimationDuration)
			 .setFrom(new Vector3(-m_Joan.rectTransform.rect.width, 0))
			.setEaseOutBack();

		LeanTween.move(m_Enemy.rectTransform, Vector3.zero, RevealAnimationDuration)
			.setFrom(new Vector3(m_Enemy.rectTransform.rect.width, 0))
			.setEaseOutBack();

		LeanTween.move(m_HeartMeter.rectTransform, Vector3.zero, RevealAnimationDuration)
			.setFrom(new Vector3(0, m_HeartMeter.rectTransform.rect.height))
			.setEaseOutBack();

		Battle.OnButtonSmashed += OnButtonSmashed;
		Battle.OnStanceChanged += OnStanceChanged;
		Battle.OnSideChanged += OnSideChanged;

		m_Cursor.rectTransform.anchoredPosition = Vector2.zero;

		m_Stance.sprite = GetButtonSprite(Battle.Stance);

		m_Joan.sprite = m_JoanNeutral;
		m_Enemy.sprite = m_EnemyHappy;
	}

	private void OnDisable()
	{
		if (Battle == null)
		{
			return;
		}

		Battle.OnButtonSmashed -= OnButtonSmashed;
		Battle.OnStanceChanged -= OnStanceChanged;
		Battle.OnSideChanged -= OnSideChanged;
	}

	private void OnButtonSmashed(Button button)
	{
		SmashButton(GetButtonImage(button));

		LeanTween.cancel(m_Cursor.gameObject);
		LeanTween.move(m_Cursor.rectTransform, new Vector3(-Battle.HeartMeter * MaxCursorDisplacement, 0), 0.1f)
			.setEaseOutBack();
	}

	private void OnStanceChanged()
	{
		m_Stance.sprite = GetButtonSprite(Battle.Stance);

		var changeStanceDuration = Battle.ChangeStanceTime - Time.time;
		var fadeOutDelay = 0.4f;

		m_Stance.color = Color.white;
		LeanTween.scale(m_Stance.rectTransform, Vector3.one, 0.1f)
			.setEaseInBack()
			.setFrom(Vector3.one * 1.25f);

		LeanTween.scale(m_Stance.rectTransform, Vector3.one * 0.5f, changeStanceDuration - fadeOutDelay)
			.setDelay(fadeOutDelay);
		LeanTween.alpha(m_Stance.rectTransform, 0.5f, changeStanceDuration - fadeOutDelay)
			.setFrom(1)
			.setEaseInQuad()
			.setDelay(fadeOutDelay);
	}

	private void OnSideChanged()
	{
		if (Battle.Side == Side.FullHeart)
		{
			m_Joan.sprite = m_JoanPlay;
			m_Enemy.sprite = m_EnemySad;
		}
		else if (Battle.Side == Side.BrokenHeart)
		{
			m_Joan.sprite = m_JoanNeutral;
			m_Enemy.sprite = m_EnemyHappy;
		}
	}

	private void SmashButton(Image button)
	{
		LeanTween.cancel(button.gameObject);
		LeanTween.scale(button.rectTransform, Vector3.one, 0.1f)
			.setEaseInBack()
			.setFrom(Vector3.one * 1.25f);
	}

	private Image GetButtonImage(Button button)
	{
		return button switch
		{
			Button.A => m_ControlA,
			Button.B => m_ControlB,
			Button.C => m_ControlC,
			_ => throw new System.NotImplementedException()
		};
	}

	private Sprite GetButtonSprite(Button button)
	{
		return GetButtonImage(button).sprite;
	}
}
