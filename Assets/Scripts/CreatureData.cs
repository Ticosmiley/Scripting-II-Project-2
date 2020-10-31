using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCreature", menuName = "CreatureData/Creature")]
public class CreatureData : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] int _maxHealth = 1;
    public int MaxHealth => _maxHealth;

    [SerializeField] int _attack = 1;
    public int Attack => _attack;

    [SerializeField] CreatureEffect _effect = null;
    public CreatureEffect Effect => _effect;
}
