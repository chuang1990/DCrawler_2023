using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Mirror : MonoBehaviour, IInteractable
{
    public UnityEvent Interacted;

    public DoorColor DoorColor;
    public string Message;

    public string PopupMessage => Message;

 //   private Material m_Material;

	//private void Awake()
	//{
 //       m_Material = GetComponent<MeshRenderer>().sharedMaterial;
	//}

	public void Interact(GameObject interactor)
    {
        Interacted?.Invoke();

		var doors = GameObject.FindGameObjectsWithTag("Door");
        
        foreach (var door in doors.Select(x => x.GetComponent<Door>()))
        {
			if (door.DoorColor == DoorColor)
            {
                door.Open();
			}

            //var doorMaterial = door.GetComponent<MeshRenderer>().sharedMaterial;

            //if (doorMaterial == m_Material)
            //{
            //    Destroy(door.gameObject);
            //}
        }
	}
}
