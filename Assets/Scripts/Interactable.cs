using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
	public UnityEvent<GameObject> OnInteracted;
	[TextArea]
	public string Message;

	public string PopupMessage => Message;

	public void Interact(GameObject interactor)
	{
		OnInteracted?.Invoke(interactor);
	}
}
