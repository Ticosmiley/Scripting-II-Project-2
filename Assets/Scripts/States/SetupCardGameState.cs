using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int _startingCardNumber = 10;
    [SerializeField] int _startingHandSize = 3;
    [SerializeField] int _numberOfPlayers = 2;

    [SerializeField] List<AbilityCardData> _abilityCards = new List<AbilityCardData>();
    [SerializeField] DeckTester _tester;

    [SerializeField] GameObject _gameVisuals;
 
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");

        Opponent.instance.gameObject.SetActive(true);
        Player.instance.gameObject.SetActive(true);
        Opponent.instance.Reset();
        Player.instance.Reset();
        

        _tester._abilityDeckConfig.Clear();

        for (int i = 0; i < _startingCardNumber; i++)
        {
            _tester._abilityDeckConfig.Add(_abilityCards[Random.Range(0, _abilityCards.Count)]);
        }

        _tester.SetupAbilityDeck();

        for (int i = 0; i < _startingHandSize; i++)
        {
            _tester.Draw();
        }

        _gameVisuals.SetActive(true);
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
