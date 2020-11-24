using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamagePlayEffect", menuName = "CardData/PlayEffects/Damage")]
public class DamagePlayEffect : CardEffect
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] GameObject _projectile;

    public override void Activate(ITargetable target)
    {
        IDamageable objectToDamage = target as IDamageable;

        if (objectToDamage != null)
        {
            PlayerTurnCardGameState playerTurn = StateMachine.CurrentState as PlayerTurnCardGameState;
            Projectile proj;
            if (playerTurn != null)
            {
                proj = Instantiate(_projectile, Player.instance.transform.position, Quaternion.identity).GetComponent<Projectile>();
            }
            else
            {
                proj = Instantiate(_projectile, Opponent.instance.transform.position, Quaternion.identity).GetComponent<Projectile>();
            }
            proj.SetTarget(target, _damageAmount);

            //objectToDamage.TakeDamage(_damageAmount);
        }
        else
        {
            Debug.Log("Target is not damageable...");
        }
    }
}
