using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Opponent : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] int _maxHealth;
    int _currentHealth;

    public int MaxHealth { get { return _maxHealth; } }
    public int CurrentHealth { get { return _currentHealth; } }

    public static Opponent instance;

    public event Action OnOpponentDeath = delegate { };

    void Awake()
    {
        instance = this;
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
    }

    public void Target()
    {
        TargetController.CurrentTarget = this as ITargetable;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Took " + amount + " damage. Remaining health: " + _currentHealth);
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Kill()
    {
        Debug.Log("Opponent died.");
        OnOpponentDeath?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
    }
}
