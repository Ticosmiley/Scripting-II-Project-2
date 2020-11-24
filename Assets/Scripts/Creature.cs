using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    public event Action<Creature> OnDeath = delegate { };
    public event Action<Creature> OnTakeDamage = delegate { };

    public string Name { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int Attack { get; private set; }
    public CreatureEffect Effect { get; private set; }
    public int boardIndex;
    public bool isEnemy = false;
    public bool canAttack = false;
    bool _selected;

    public bool Selected { get { return _selected; } set { _selected = value; } }

    RaycastHit hit;
    PlayerTurnCardGameState _playerTurn;

    [SerializeField] CreatureData _data;

    private void Awake()
    {
        _playerTurn = FindObjectOfType<PlayerTurnCardGameState>();

        Name = _data.Name;
        MaxHealth = _data.MaxHealth;
        CurrentHealth = MaxHealth;
        Attack = _data.Attack;
        Effect = _data.Effect;

        if (Effect != null)
        {
            ITargetable target = TargetController.CurrentTarget;
            Effect.Activate(target);
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.GetComponent<Creature>() == this && !(_playerTurn.targeting || _playerTurn.targetingAttack) && canAttack && !isEnemy)
            {
                _selected = true;
            }
            else if(hit.collider.gameObject.GetComponent<Creature>() == this && _playerTurn.targetingAttack && isEnemy)
            {
                _selected = true;
            }
            else
            {
                _selected = false;
            }
        }
        else
        {
            _selected = false;
        }
    }

    public void Kill()
    {
        OnDeath?.Invoke(this);
        Debug.Log("Kill the creature!");
        //gameObject.SetActive(false);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Took " + damage + " damage. Remaining health: " + CurrentHealth);
        OnTakeDamage?.Invoke(this);
        if (CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public virtual void Heal(int healAmount)
    {
        int temp = CurrentHealth;
        CurrentHealth += healAmount;
        
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            Debug.Log("Healed for " + (MaxHealth - temp) + " health. Current health: " + CurrentHealth);
        }
        else
        {
            Debug.Log("Healed for " + healAmount + " health. Current health: " + CurrentHealth);
        }
    }

    public virtual void BuffAttack(int amount)
    {
        Attack += amount;
    }

    public virtual void BuffHealth(int amount)
    {
        MaxHealth += amount;
        CurrentHealth += amount;
    }

    public void Target()
    {
        Debug.Log("Creature has been targeted.");
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
