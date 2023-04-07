using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomTrigger : MonoBehaviour
{
    public UnityEvent Triggered;
    public float MinInterval = 10;
    public float MaxInterval = 25;

	private void Start()
	{
		LeanTween.delayedCall(Random.Range(MinInterval, MaxInterval), OnTriggered);
	}

	private void OnTriggered()
	{
		if (enabled)
		{
			Triggered?.Invoke();
		}

		LeanTween.delayedCall(Random.Range(MinInterval, MaxInterval), OnTriggered);
	}
}
