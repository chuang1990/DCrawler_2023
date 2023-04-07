using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashEventPlayer : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter m_ButtonMashEventEmitter;

    public void Play(Button button)
    {
		m_ButtonMashEventEmitter.EventInstance.setParameterByNameWithLabel("Instmash", GetInstmashLabel(button));
		m_ButtonMashEventEmitter.Play();
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
