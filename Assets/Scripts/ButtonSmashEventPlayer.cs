using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSmashEventPlayer : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter m_ButtonSmashEventEmitter;

    public void Play(Button button)
    {
		m_ButtonSmashEventEmitter.EventInstance.setParameterByNameWithLabel("Instmash", GetInstmashLabel(button));
		m_ButtonSmashEventEmitter.Play();
	}

	private string GetInstmashLabel(Button button)
	{
		return button switch
		{
			Button.A => "Drums",
			Button.B => "Piano",
			Button.C => "Guitar",
			_ => throw new System.NotImplementedException()
		};
	}
}
