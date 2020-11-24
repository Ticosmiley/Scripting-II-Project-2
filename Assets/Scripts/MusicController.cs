using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource _menuMusic;
    [SerializeField] AudioSource _gameMusic;
    [SerializeField] AudioSource _loseMusic;
    [SerializeField] AudioSource _winMusic;

    [SerializeField] SetupCardGameState _setup;
    [SerializeField] WinCardGameState _win;
    [SerializeField] LoseCardGameState _lose;
    [SerializeField] MainMenuCardGameState _menu;

    private void OnEnable()
    {
        _setup.GameStart += OnGameStart;
        _menu.MainMenu += OnMenu;
        _win.Win += OnWin;
        _lose.Lose += OnLose;
    }

    void OnGameStart()
    {
        _menuMusic.Stop();
        _gameMusic.Play();
    }

    void OnMenu()
    {
        _winMusic.Stop();
        _loseMusic.Stop();
        _menuMusic.Play();
    }

    void OnWin()
    {
        _gameMusic.Stop();
        _winMusic.Play();
    }

    void OnLose()
    {
        _gameMusic.Stop();
        _loseMusic.Play();
    }
}
