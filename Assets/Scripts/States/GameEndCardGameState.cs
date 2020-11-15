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
        GetComponent<PlayerTurnCardGameState>().playerTurnCount = 0;

        for (int i = 0; i < 5; i++)
        {
            _playerHandView._slotsFilled[i] = false;
            _spawnManager._friendlySlotsFilled[i] = false;
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
        foreach (var item in _spawnManager.friendlySpawns)
        {
            Destroy(item);
        }
        _spawnManager.friendlySpawns.Clear();
        foreach (var item in _spawnManager._friendlySpawnViews)
        {
            Destroy(item.gameObject);
        }
        _spawnManager._friendlySpawnViews.Clear();
        foreach (var item in _spawnManager.enemySpawns)
        {
            Destroy(item);
        }
        _spawnManager.enemySpawns.Clear();
        foreach (var item in _spawnManager._enemySpawnViews)
        {
            Destroy(item.gameObject);
        }
        _spawnManager._enemySpawnViews.Clear();
    }

    public override void Tick()
    {
        if (Player.instance.CurrentHealth <= 0)
        {
            StateMachine.ChangeState<LoseCardGameState>();
        }
        else
        {
            StateMachine.ChangeState<WinCardGameState>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting game end");
    }
}
