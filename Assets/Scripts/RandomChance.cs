using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomChance : MonoBehaviour
{
    public UnityEvent Triggered;
    [Range(0, 1)]
    public float Chance = 0.5f;

    public void Play()
    {
        if (Random.value < Chance)
        {
            Triggered?.Invoke();
        }
    }
}
