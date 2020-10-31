using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattlecryEffect", menuName = "CreatureData/CreatureEffects/Battlecry")]
public class BattlecryCreatureEffect : CreatureEffect
{
    [SerializeField] List<Effect> _effects = new List<Effect>();

    public override void Activate(ITargetable target = null)
    {
        foreach (Effect effect in _effects)
        {
            switch (effect.Type)
            {
                case EffectType.Damage:
                    var damageTarget = target as IDamageable;
                    if (damageTarget != null)
                    {
                        damageTarget.TakeDamage(effect.Value);
                    }
                    break;
                case EffectType.Heal:
                    var healTarget = target as IDamageable;
                    if (healTarget != null)
                    {
                        healTarget.Heal(effect.Value);
                    }
                    break;
                case EffectType.PermanentDamageBuff:
                    var damageBuffTarget = target as Creature;
                    if (damageBuffTarget != null)
                    {
                        damageBuffTarget.BuffAttack(effect.Value);
                    }
                    break;
                case EffectType.PermanentHealthBuff:
                    var healthBuffTarget = target as Creature;
                    if (healthBuffTarget != null)
                    {
                        healthBuffTarget.BuffHealth(effect.Value);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
