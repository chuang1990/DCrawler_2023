using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent Healed;
    public UnityEvent Damaged;
    public UnityEvent Died;

    public int MaxHealth = 20;

    public int Points => m_Points;
    public float Percentage => Mathf.Clamp01((float)m_Points / MaxHealth);

    private int m_Points;

    public bool ChangeHealth(int amount)
    {
        var prevHealthPoints = m_Points;

        m_Points += amount;
        m_Points = Mathf.Clamp(m_Points, 0, MaxHealth);

        if (prevHealthPoints == m_Points)
        {
            return false;
        }

        if (amount > 0)
        {
            Healed?.Invoke();
        }
        else
        {
            Damaged?.Invoke();
        }

        if (m_Points <= 0)
        {
            Died?.Invoke();
        }

        return true;
    }

	private void Start()
	{
        m_Points = MaxHealth;
	}
}
