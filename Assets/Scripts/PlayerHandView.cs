using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHandView : MonoBehaviour
{
    [SerializeField] DeckTester _tester;
    [SerializeField] GameObject _cardObject;
    [SerializeField] GameObject _cardUI;
    [SerializeField] GameObject _canvas;

    Deck<AbilityCard> _playerHand;

    [SerializeField] Transform[] _cardSlots;
    public bool[] _slotsFilled = new bool[] { false, false, false, false, false };
    public List<GameObject> _cardObjects = new List<GameObject>();
    public List<AbilityCardView> _cardViews = new List<AbilityCardView>();

    private void Awake()
    {
        _playerHand = _tester.PlayerHand;
    }

    public void AddCard(AbilityCard card)
    {
        GameObject newCard = Instantiate(_cardObject);
        AbilityCardView newACView = Instantiate(_cardUI).GetComponent<AbilityCardView>();
        newACView.transform.SetParent(_canvas.transform, false);
        newACView.Display(card, newCard.transform);


        for (int i = 0; i < 5; i++)
        {
            if (!_slotsFilled[i])
            {
                newCard.transform.position = _cardSlots[i].position;
                newCard.GetComponent<CardObject>().Setup(card, i);
                _cardObjects.Add(newCard);
                _cardViews.Add(newACView);
                _slotsFilled[i] = true;
                break;
            }
        }
    }

    public void RemoveCard(int index)
    {
        Destroy(_cardObjects[index]);
        _cardObjects.RemoveAt(index);
        Destroy(_cardViews[index].gameObject);
        _cardViews.RemoveAt(index);
        _slotsFilled[index] = false;

        for (int i = 0; i < _cardObjects.Count; i++)
        { 
            CardObject obj = _cardObjects[i].GetComponent<CardObject>();
            obj.Setup(obj.Card, i);
            obj.transform.position = _cardSlots[i].position;
            _slotsFilled[i] = true;
        }

        for (int i = 0; i < 5; i++)
        {
            if (i > _cardObjects.Count - 1)
            {
                _slotsFilled[i] = false;
            }
        }
    }
}
