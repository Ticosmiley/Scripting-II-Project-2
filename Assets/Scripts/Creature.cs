using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    public string Name { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int Attack { get; private set; }
    public CreatureEffect Effect { get; private set; }

    [SerializeField] CreatureData _data;

    private void Start()
    {
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

    public void Kill()
    {
        Debug.Log("Kill the creature!");
        gameObject.SetActive(false);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Took " + damage + " damage. Remaining health: " + CurrentHealth);
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
}
