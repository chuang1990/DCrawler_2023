using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPickup : MonoBehaviour
{
	public UnityEvent OnConsumed;

	public int HealAmount = 1;

	private void OnTriggerEnter(Collider other)
	{
		var health = other.GetComponent<Health>();

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
