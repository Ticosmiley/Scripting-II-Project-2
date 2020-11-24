using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinCardGameState : CardGameState
{
    public event Action Win = delegate { };

    [SerializeField] GameObject _winScreen;

    public override void Enter()
    {
        Win?.Invoke();

        Debug.Log("Enter win state");
        _winScreen.SetActive(true);
    }

    public override void Exit()
    {
        _winScreen.SetActive(false);
    }

    public void MainMenu()
    {
        StateMachine.ChangeState<MainMenuCardGameState>();
    }
}
