using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static ITargetable CurrentTarget;
    public static ITargetable EnemyTarget;

    private void Update()
    {
        if (StateMachine.CurrentState.GetComponent<PlayerTurnCardGameState>() == null)
        {
            CurrentTarget = null;
        }
    }
}
