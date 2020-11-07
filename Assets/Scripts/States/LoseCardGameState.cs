using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCardGameState : CardGameState
{
    [SerializeField] GameObject _loseScreen;

    public override void Enter()
    {
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
