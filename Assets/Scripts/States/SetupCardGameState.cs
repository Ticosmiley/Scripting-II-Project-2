using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetupCardGameState : CardGameState
{
    public event Action GameStart = delegate { };
    
    [SerializeField] int _startingCardNumber = 10;
    [SerializeField] int _startingHandSize = 3;
    [SerializeField] int _numberOfPlayers = 2;

    [SerializeField] List<AbilityCardData> _abilityCards = new List<AbilityCardData>();
    [SerializeField] DeckTester _tester;
    [SerializeField] EnemyDeckTester _enemyTester;

    [SerializeField] GameObject _gameVisuals;
 
    bool _activated = false;

    public override void Enter()
    {
        GameStart?.Invoke();

        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");

        Opponent.instance.gameObject.SetActive(true);
        Player.instance.gameObject.SetActive(true);
        Opponent.instance.Reset();
        Player.instance.Reset();

        _tester._abilityDeckConfig.Clear();
        _enemyTester._abilityDeckConfig.Clear();

        _gameVisuals.SetActive(true);

        for (int i = 0; i < _startingCardNumber; i++)
        {
            _tester._abilityDeckConfig.Add(_abilityCards[UnityEngine.Random.Range(0, _abilityCards.Count)]);
            _enemyTester._abilityDeckConfig.Add(_abilityCards[UnityEngine.Random.Range(0, _abilityCards.Count)]);
        }

        _tester.SetupAbilityDeck();
        _enemyTester.SetupAbilityDeck();

        for (int i = 0; i < _startingHandSize; i++)
        {
            _tester.Draw();
            _enemyTester.Draw();
        }

        _activated = false;
    }

    public override void Tick()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }
}
