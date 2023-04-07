using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class HealthPickup : MonoBehaviour
{
	public UnityEvent OnConsumed;
	public EventReference pillpickup;

	public int HealAmount = 1;

	private void OnTriggerEnter(Collider other)
	{
		var health = other.GetComponent<Health>();
        RuntimeManager.PlayOneShot(pillpickup);
        if (health == null)
		{
			return;
		}

		if (health.ChangeHealth(HealAmount))
		{
			Destroy(gameObject);

			OnConsumed?.Invoke();
		}
	}
}
