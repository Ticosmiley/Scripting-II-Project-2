using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCardGameState : CardGameState
{
    [SerializeField] GameObject _mainMenu;

    public override void Enter()
    {
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
