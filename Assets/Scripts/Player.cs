using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] int _maxHealth;
    int _currentHealth;

    public int maxMana = 0;
    public int currentMana = 0;

    public event Action OnPlayerDeath = delegate { };

    public int CurrentHealth { get { return _currentHealth; } }
    public int MaxHealth { get { return _maxHealth; } }

    public static Player instance;

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
        Debug.Log("Player died.");
        OnPlayerDeath?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
        maxMana = 0;
        currentMana = 0;
    }
}
