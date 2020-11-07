using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndCardGameState : CardGameState
{
    [SerializeField] GameObject _gameVisuals;
    [SerializeField] PlayerHandView _playerHandView;
    [SerializeField] SpawnManager _spawnManager;

    public override void Enter()
    {
        Debug.Log("Enter game end");

        _gameVisuals.SetActive(false);
        Opponent.instance.gameObject.SetActive(false);
        Player.instance.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            _playerHandView._slotsFilled[i] = false;
            _spawnManager._slotsFilled[i] = false;
        }
        foreach (var item in _playerHandView._cardObjects)
        {
            Destroy(item);
        }
        _playerHandView._cardObjects.Clear();
        foreach (var item in _playerHandView._cardViews)
        {
            Destroy(item.gameObject);
        }
        _playerHandView._cardViews.Clear();
        foreach (var item in _spawnManager._friendlySpawns)
        {
            Destroy(item);
        }
        _spawnManager._friendlySpawns.Clear();
        foreach (var item in _spawnManager._spawnViews)
        {
            Destroy(item.gameObject);
        }
        _spawnManager._spawnViews.Clear();
    }

    public override void Tick()
    {
        if (Player.instance.CurrentHealth <= 0)
        {
            Debug.Log("Lose");
            StateMachine.ChangeState<LoseCardGameState>();
        }
        else
        {
            Debug.Log("Win");
            StateMachine.ChangeState<WinCardGameState>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting game end");
    }
}
