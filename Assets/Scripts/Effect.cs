using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Effects/Effects")]
public class Effect : ScriptableObject
{
    [SerializeField] TargetType _target = TargetType.Single;
    public TargetType Target => _target;
    [SerializeField] EffectType _type = default;
    public EffectType Type => _type;
    [SerializeField] int _value = 1;
    public int Value => _value;
}
