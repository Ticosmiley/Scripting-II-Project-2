using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    AbilityCard _card;
    int _index;
    bool _selected;

    public AbilityCard Card { get { return _card; } }
    public int Index { get { return _index; } }
    public bool Selected { get { return _selected; } set { _selected = value; } }

    RaycastHit hit;
    PlayerTurnCardGameState _playerTurn;

    private void Awake()
    {
        _playerTurn = FindObjectOfType<PlayerTurnCardGameState>();
    }

    public void Setup(AbilityCard card, int index)
    {
        _card = card;
        _index = index;
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.GetComponent<CardObject>() == this && !(_playerTurn.targeting || _playerTurn.targetingAttack) && _card.Cost <= Player.instance.currentMana)
            {
                _selected = true;
            }
            else
            {
                _selected = false;
            }
        }
        else
        {
            _selected = false;
        }
    }
}
