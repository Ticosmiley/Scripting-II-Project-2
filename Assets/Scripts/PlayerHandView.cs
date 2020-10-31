using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHandView : MonoBehaviour
{
    AbilityCardView _acView;
    [SerializeField] DeckTester _tester;

    TextMeshProUGUI _cardsInHand;

    int _currentCardIndex = 0;

    public int CurrentCardIndex { get { return _currentCardIndex; } }

    private void Awake()
    {
        _acView = GetComponentInChildren<AbilityCardView>();
        _acView.gameObject.SetActive(false);
        _cardsInHand = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Display(AbilityCard card, int index)
    {
        _acView.gameObject.SetActive(true);
        _currentCardIndex = index;
        _acView.Display(card);
        _cardsInHand.text = "Cards in Hand: " + _tester.PlayerHand.Count;
    }

    public void Next()
    {
        if (!CheckIfEmpty())
        {
            if (_currentCardIndex + 1 < _tester.PlayerHand.Count)
            {
                _currentCardIndex++;
            }
            else
            {
                _currentCardIndex = 0;
            }
            _acView.Display(_tester.PlayerHand.GetCard(_currentCardIndex));
        }
        _cardsInHand.text = "Cards in Hand: " + _tester.PlayerHand.Count;
    }

    public void Prev()
    {
        if (!CheckIfEmpty())
        {
            if (_currentCardIndex - 1 >= 0)
            {
                _currentCardIndex--;
            }
            else
            {
                _currentCardIndex = _tester.PlayerHand.Count - 1;
            }
            _acView.Display(_tester.PlayerHand.GetCard(_currentCardIndex));
        }
        _cardsInHand.text = "Cards in Hand: " + _tester.PlayerHand.Count;
    }

    bool CheckIfEmpty()
    {
        if (_tester.PlayerHand.IsEmpty)
        {
            _acView.gameObject.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }
}
