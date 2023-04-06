using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string PopupMessage { get; }
    void Interact(GameObject interactor);
}
