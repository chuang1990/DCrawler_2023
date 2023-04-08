using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private GameObject m_BattleUI;
	[SerializeField]
	private TMP_Text m_Interactable;
	[SerializeField]
	private Image m_HealthBar;
	[SerializeField]
	private GameObject m_GameOver;
	private PlayerController m_Player;
	private Health m_PlayerHealth;

	private void Start()
	{
		GameEvents.Instance.OnBattleInitiated.AddListener(OnBattleInitiated);
		GameEvents.Instance.OnBattleFinished.AddListener(OnBattleFinished);

		m_BattleUI.SetActive(false);
	}

	private void Awake()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		m_PlayerHealth = m_Player.GetComponent<Health>();

		m_PlayerHealth.Died.AddListener(OnPlayerDied);

		//m_Interactable.gameObject.SetActive(false);
	}

	private void OnPlayerDied()
	{
		m_BattleUI.SetActive(false);
		m_GameOver.SetActive(true);
	}

	private void Update()
	{
		m_Interactable.text = m_Player.Interactable != null ? "!!" : "";

		//if (m_Player.Interactable != null)
		//{
		//	m_Interactable.gameObject.SetActive(true);
		//	m_Interactable.text = "!!";
		//}
		//else
		//{
		//	m_Interactable.gameObject.SetActive(false);
		//}

		m_HealthBar.fillAmount = m_PlayerHealth.Percentage;
	}

	public void OnBattleInitiated(EnemyController enemy)
	{
		m_BattleUI.SetActive(true);
	}

	public void OnBattleFinished(EnemyController enemy, BattleResult result)
	{
		m_BattleUI.SetActive(false);
	}
	
	public void Retry()
	{
		SceneManager.LoadScene(1);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
