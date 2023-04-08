using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private Animator myAnimCon;
    private void OnTriggerEnter(Collider other)
    {
        myAnimCon.SetBool("Credits", true);
    }
}
