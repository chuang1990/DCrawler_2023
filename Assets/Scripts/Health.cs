using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHealed;
    public UnityEvent OnDamaged;

    public int MaxHealth = 10;

    public int Points => m_Points;

    private int m_Points;

    public bool ChangeHealth(int amount)
    {
        var previousPoints = m_Points;

        m_Points += amount;
        m_Points = Mathf.Clamp(m_Points, 0, MaxHealth);

        if (previousPoints == m_Points)
        {
            return false;
        }

        if (amount > 0)
        {
            OnHealed?.Invoke();
        }
        else
        {
            OnDamaged?.Invoke();
        }

        return true;
    }

	private void Start()
	{
        m_Points = MaxHealth;
	}
}
