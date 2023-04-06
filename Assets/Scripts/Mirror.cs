using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    public string Message;

    public string PopupMessage => Message;

    private Material m_Material;

	private void Awake()
	{
        m_Material = GetComponent<MeshRenderer>().sharedMaterial;
	}

	public void Interact(GameObject interactor)
    {
        var doors = GameObject.FindGameObjectsWithTag("Door");
        
        foreach (var door in doors)
        {
            var doorMaterial = door.GetComponent<MeshRenderer>().sharedMaterial;

            if (doorMaterial == m_Material)
            {
                Destroy(door.gameObject);
            }
        }
	}
}
