using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCardGameState : CardGameState
{
    [SerializeField] GameObject _winScreen;

    public override void Enter()
    {
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
