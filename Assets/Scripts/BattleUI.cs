using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public UnityEvent OnRan;

    [SerializeField]
    private Button m_Run;

	private void Start()
	{
		m_Run.onClick.AddListener(() => OnRan?.Invoke());
	}
}
