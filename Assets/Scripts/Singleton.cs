using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			if (s_Instance == null)
			{
				var gameObject = new GameObject(nameof(T));
				s_Instance = gameObject.AddComponent<T>();
			}

			return s_Instance;
		}
	}

	private static T s_Instance;
}
