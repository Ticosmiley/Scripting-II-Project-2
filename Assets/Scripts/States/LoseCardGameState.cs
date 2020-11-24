using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoseCardGameState : CardGameState
{
    public event Action Lose = delegate { };

    [SerializeField] GameObject _loseScreen;

    public override void Enter()
    {
        Lose?.Invoke();

        _loseScreen.SetActive(true);
    }

    public override void Exit()
    {
        _loseScreen.SetActive(false);
    }

    public void MainMenu()
    {
        StateMachine.ChangeState<MainMenuCardGameState>();
    }
}
