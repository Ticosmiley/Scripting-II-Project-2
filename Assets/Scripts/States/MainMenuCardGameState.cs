using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuCardGameState : CardGameState
{
    public event Action MainMenu = delegate { };

    [SerializeField] GameObject _mainMenu;

    public override void Enter()
    {
        MainMenu?.Invoke();
        _mainMenu.SetActive(true);
    }

    public override void Exit()
    {
        _mainMenu.SetActive(false);
    }

    public void StartGame()
    {
        StateMachine.ChangeState<SetupCardGameState>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
