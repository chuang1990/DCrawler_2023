using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public UnityEvent<Button> ButtonMashed;
	public UnityEvent<Side> SideChanged;
	public UnityEvent<Button> StanceChanged;
	public UnityEvent<BattleResult> BattleFinished;
	public float RevealAnimationDuration = 0.75f;
	public float MaxCursorDisplacement = 120;
	public bool isHit = false;

	public enum EnemyType { A, B, C, D};
	public EnemyType enemy_type;

	[HideInInspector]
	public Battle Battle;

	[Header("Sprites")]
	[SerializeField]
	private Sprite m_JoanPlay;
	[SerializeField]
	private Sprite m_JoanNeutral;
	[SerializeField]
	private Sprite m_JoanSad;
	[SerializeField]
	private Sprite m_JoanHappy;
	[SerializeField]
	private Sprite m_JoanHit;
	Sprite m_EnemyHappy;
	Sprite m_EnemySad;
	[SerializeField]
	private Sprite m_EnemySadA;
	[SerializeField]
	private Sprite m_EnemyHappyA;
	[SerializeField]
	private Sprite m_EnemySadB;
	[SerializeField]
	private Sprite m_EnemyHappyB;
	[SerializeField]
	private Sprite m_EnemySadC;
	[SerializeField]
	private Sprite m_EnemyHappyC;
	[SerializeField]
	private Sprite m_EnemySadD;
	[SerializeField]
	private Sprite m_EnemyHappyD;

	[Header("UI Elements")]
	[SerializeField]
	private RectTransform m_HeartMeterContainer;
	[SerializeField]
	private RectTransform m_JoanContainer;
	[SerializeField]
	private RectTransform m_EnemyContainer;
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
	private Dictionary<Image, Vector3> m_DefaultScales = new Dictionary<Image, Vector3>();

	private void Start()
	{
		foreach (var image in GetComponentsInChildren<Image>())
		{
			m_DefaultScales[image] = image.rectTransform.localScale;
		}
	}

	private void OnEnable()
	{
		if (Battle == null)
		{
			return;
		}

		LeanTween.move(m_JoanContainer, Vector3.zero, RevealAnimationDuration)
			 .setFrom(new Vector3(-m_JoanContainer.rect.width, 0))
			.setEaseOutBack();

		LeanTween.move(m_EnemyContainer, Vector3.zero, RevealAnimationDuration)
			.setFrom(new Vector3(m_EnemyContainer.rect.width, 0))
			.setEaseOutBack();

		LeanTween.move(m_HeartMeterContainer, Vector3.zero, RevealAnimationDuration)
			.setFrom(new Vector3(0, m_HeartMeterContainer.rect.height))
			.setEaseOutBack();

		Battle.OnButtonMashed += OnButtonMashed;
		Battle.OnStanceChanged += OnStanceChanged;
		Battle.OnSideChanged += OnSideChanged;
		Battle.OnBattleFinished += OnBattleFinished;

		m_Cursor.rectTransform.anchoredPosition = Vector2.zero;

		m_Stance.sprite = GetButtonSprite(Battle.Stance);

		

		switch (enemy_type)
        {
			case EnemyType.A:
				m_EnemyHappy = m_EnemyHappyA;
				m_EnemySad = m_EnemySadA;
				break;
			case EnemyType.B:
				m_EnemyHappy = m_EnemyHappyB;
				m_EnemySad = m_EnemySadB;
				break;
			case EnemyType.C:
				m_EnemyHappy = m_EnemyHappyC;
				m_EnemySad = m_EnemySadC;
				break;
			case EnemyType.D:
				m_EnemyHappy = m_EnemyHappyD;
				m_EnemySad = m_EnemySadD;
				break;
			default:
				m_EnemyHappy = m_EnemyHappyA;
				m_EnemySad = m_EnemySadA;
				break;
		}

		m_Joan.sprite = m_JoanNeutral;
		m_Enemy.sprite = m_EnemyHappy;
	}

	private void OnDisable()
	{
		if (Battle == null)
		{
			return;
		}

		Battle.OnButtonMashed -= OnButtonMashed;
		Battle.OnStanceChanged -= OnStanceChanged;
		Battle.OnSideChanged -= OnSideChanged;
		Battle.OnBattleFinished -= OnBattleFinished;
	}

	private void OnButtonMashed(Button button)
	{
		ButtonMashed?.Invoke(button);

		MashButton(GetButtonImage(button));

		LeanTween.cancel(m_Cursor.gameObject);
		LeanTween.move(m_Cursor.rectTransform, new Vector3(-Battle.HeartMeter * MaxCursorDisplacement, 0), 0.1f)
			.setEaseOutBack();
	}

	private void OnStanceChanged()
	{
		StanceChanged?.Invoke(Battle.Stance);

		m_Stance.sprite = GetButtonSprite(Battle.Stance);

		var changeStanceDuration = Battle.ChangeStanceTime - Time.time;
		var fadeOutDelay = 0.4f;

		LeanTween.scale(m_Stance.rectTransform, Vector3.one, 0.1f)
			.setEaseInBack()
			.setFrom(Vector3.one * 1.25f);
		LeanTween.alpha(m_Stance.rectTransform, 1, 0.01f);

		LeanTween.scale(m_Stance.rectTransform, Vector3.one * 0.5f, changeStanceDuration - fadeOutDelay)
			.setDelay(fadeOutDelay);
		LeanTween.alpha(m_Stance.rectTransform, 0.5f, changeStanceDuration - fadeOutDelay)
			.setEaseInQuad()
			.setDelay(fadeOutDelay);
	}

	private void OnSideChanged()
	{
		SideChanged?.Invoke(Battle.Side);
		if(isHit == true){
			m_Joan.sprite = m_JoanHit;
			m_Enemy.sprite = m_EnemyHappy;
			isHit = false;
		}
		else
		{
			if (Battle.Side == Side.BrokenHeart)
			{
				m_Joan.sprite = m_JoanSad;
				m_Enemy.sprite = m_EnemySad;
			}
			else if (Battle.Side == Side.FullHeart)
			{
				m_Joan.sprite = m_JoanHappy;
				m_Enemy.sprite = m_EnemyHappy;
			}
			else if (Battle.Side == Side.Neutral)
			{
				m_Joan.sprite = m_JoanNeutral;
			}
		}
		JoltImage(m_Joan);
		JoltImage(m_Enemy);
	}

	private void OnBattleFinished(BattleResult result)
	{
		BattleFinished?.Invoke(result);
	}

	private void JoltImage(Image image)
	{
		var scale = GetDefaultScale(image);

		LeanTween.cancel(image.gameObject);
		LeanTween.scale(image.rectTransform, scale, 0.1f)
			.setEaseInBack()
			.setFrom(scale * 1.1f);
	}

	private void MashButton(Image button)
	{
		var scale = GetDefaultScale(button);

		LeanTween.cancel(button.gameObject);
		LeanTween.scale(button.rectTransform, scale, 0.1f)
			.setEaseInBack()
			.setFrom(scale * 1.25f);
	}

	private Vector3 GetDefaultScale(Image image)
	{
		return m_DefaultScales.GetValueOrDefault(image);
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
