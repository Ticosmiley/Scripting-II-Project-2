using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI = null;
    [SerializeField] DeckTester _tester;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _tester.Draw();

        Player.instance.maxMana++;
        Player.instance.currentMana = Player.instance.maxMana;

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        Player.instance.OnPlayerDeath += OnPlayerDeath;
        Opponent.instance.OnOpponentDeath += OnOpponentDeath;
    }

    public override void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CardObject card = hit.collider.gameObject.GetComponent<CardObject>();
                if (card != null)
                {
                    Debug.Log("Playing card " + card.Card.Name);
                    _tester.currentCard = card.Index;
                    _tester.PlayCard();
                }

                ITargetable target = hit.collider.gameObject.GetComponent<ITargetable>();
                if (target != null)
                {
                    Debug.Log("Current target is " + target.ToString());
                    TargetController.CurrentTarget = target;
                }
            }
        }
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        Player.instance.OnPlayerDeath -= OnPlayerDeath;
        Opponent.instance.OnOpponentDeath -= OnOpponentDeath;

        Debug.Log("Player Turn: Exiting...");
    }

    public void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    void OnPlayerDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }

    void OnOpponentDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }
}
