using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] TextMeshProUGUI _enemyThinkingTextUI;
    [SerializeField] EnemyDeckTester _tester;
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] float _pauseDuration = 1.5f;
    
    public override void Enter()
    {
        Player.instance.OnPlayerDeath += OnPlayerDeath;
        Opponent.instance.OnOpponentDeath += OnOpponentDeath;

        _enemyThinkingTextUI.gameObject.SetActive(true);

        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        _tester.Draw();

        foreach (var creature in _spawnManager.enemySpawns)
        {
            creature.GetComponent<Creature>().canAttack = true;
        }

        Opponent.instance.maxMana++;
        Opponent.instance.currentMana = Opponent.instance.maxMana;

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }


    public override void Exit()
    {
        _enemyThinkingTextUI.gameObject.SetActive(false);
        Player.instance.OnPlayerDeath -= OnPlayerDeath;
        Opponent.instance.OnOpponentDeath -= OnOpponentDeath;

        Debug.Log("Enemy Turn: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        yield return StartCoroutine(_tester.PlayCards());
        yield return StartCoroutine(_spawnManager.EnemyAttacks());

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
