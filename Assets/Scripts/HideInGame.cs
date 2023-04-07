using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGame : MonoBehaviour
{
	private void Start()
	{
		Destroy(gameObject);
	}
}
