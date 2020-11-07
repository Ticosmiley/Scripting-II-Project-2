using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;
    
    public override void Enter()
    {
        Player.instance.OnPlayerDeath += OnPlayerDeath;
        Opponent.instance.OnOpponentDeath += OnOpponentDeath;

        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }


    public override void Exit()
    {
        Player.instance.OnPlayerDeath -= OnPlayerDeath;
        Opponent.instance.OnOpponentDeath -= OnOpponentDeath;

        Debug.Log("Enemy Turn: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        TargetController.EnemyTarget = Player.instance;
        Player.instance.TakeDamage(1);

        Debug.Log("Enemy attacks players for 3 damage!");
        EnemyTurnEnded?.Invoke();

        StateMachine.ChangeState<PlayerTurnCardGameState>();
    }

    void OnPlayerDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }

    void OnOpponentDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }
}
