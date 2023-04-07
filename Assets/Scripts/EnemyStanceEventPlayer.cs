using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStanceEventPlayer : MonoBehaviour
{
	[SerializeField]
	private StudioEventEmitter m_EnemyStanceEventEmitter;

	public void Play(Button button)
	{
		m_EnemyStanceEventEmitter.EventInstance.setParameterByNameWithLabel("Enemyloops", GetEnemyLoopsLabel(button));
	}

	private string GetEnemyLoopsLabel(Button button)
	{
		return button switch
		{
			Button.A => "Drumloop",
			Button.B => "Pianoloop",
			Button.C => "Guitarloop",
			_ => throw new System.NotImplementedException()
		};
	}
}
