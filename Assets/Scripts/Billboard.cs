using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		var camera = Camera.main;
		transform.LookAt(transform.position + camera.transform.forward);
	}
}
