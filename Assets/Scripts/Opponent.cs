using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Opponent : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] int _maxHealth;
    int _currentHealth;

    public int maxMana = 0;
    public int currentMana = 0;

    public int MaxHealth { get { return _maxHealth; } }
    public int CurrentHealth { get { return _currentHealth; } }

    [SerializeField] GameObject _targetingReticle;

    public static Opponent instance;

    public event Action OnOpponentDeath = delegate { };

    RaycastHit hit;
    PlayerTurnCardGameState _playerTurn;

    void Awake()
    {
        instance = this;
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
        _playerTurn = FindObjectOfType<PlayerTurnCardGameState>();
    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.GetComponent<Opponent>() == this && _playerTurn.targetingAttack)
            { 
                _targetingReticle.SetActive(true);
            }
            else
            {
                _targetingReticle.SetActive(false);
            }
        }
        else
        {
            _targetingReticle.SetActive(false);
        }
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
        maxMana = 0;
        currentMana = 0;
    }

    public bool IsEnemy()
    {
        return true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
