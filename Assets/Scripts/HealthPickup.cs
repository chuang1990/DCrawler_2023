using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	public int HealAmount = 1;

	private void OnTriggerEnter(Collider other)
	{
		var health = other.GetComponent<Health>();

		if (health != null)
		{
			if (health.ChangeHealth(HealAmount))
			{
				Destroy(gameObject);
			}
		}
	}
}
