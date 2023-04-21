using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float Health;

    public UnityEvent OnDeath;

    public void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
