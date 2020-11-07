using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckTester : MonoBehaviour
{
    public List<AbilityCardData> _abilityDeckConfig = new List<AbilityCardData>();

    Deck<AbilityCard> _abilityDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityDiscard = new Deck<AbilityCard>();

    Deck<AbilityCard> _playerHand = new Deck<AbilityCard>();

    [SerializeField] PlayerHandView _playerHandView;

    public int currentCard;

    public Deck<AbilityCard> PlayerHand { get { return _playerHand; } }
    public Deck<AbilityCard> AbilityDeck { get { return _abilityDeck; } }
    public Deck<AbilityCard> AbilityDiscard { get { return _abilityDiscard; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetupAbilityDeck()
    {
        _playerHand.Clear();
        _abilityDiscard.Clear();
        _abilityDeck.Clear();

        foreach (AbilityCardData abilityData in _abilityDeckConfig)
        {
            AbilityCard newAbilityCard = new AbilityCard(abilityData);
            _abilityDeck.Add(newAbilityCard);
        }

        _abilityDeck.Shuffle();
    }

    public void Draw()
    {
        if (_abilityDeck.IsEmpty)
        {
            while (!_abilityDiscard.IsEmpty)
            {
                _abilityDeck.Add(_abilityDiscard.GetCard(0));
                _abilityDiscard.Remove(0);
            }
            _abilityDeck.Shuffle();
        }

        AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
        Debug.Log("Drew card: " + newCard.Name);
        if (_playerHand.Count < 5)
        {
            _playerHand.Add(newCard, DeckPosition.Top);
            _playerHandView.AddCard(newCard);
        }
        else
        {
            _abilityDiscard.Add(newCard, DeckPosition.Top);
            Debug.Log("Hand full. Discarded " + newCard.Name);
        }
    }

    private void PrintPlayerHand()
    {
        for (int i = 0; i < _playerHand.Count; i++)
        {
            Debug.Log("Player Hand Card: " + _playerHand.GetCard(i).Name);
        }
    }

    public void PlayCard()
    {
        if (TargetController.CurrentTarget != null)
        {
            AbilityCard targetCard = _playerHand.GetCard(currentCard);
            if (targetCard.Cost <= Player.instance.currentMana)
            {
                    
                _playerHandView.RemoveCard(currentCard);
                _playerHand.Remove(currentCard);
                _abilityDiscard.Add(targetCard);
                Player.instance.currentMana -= targetCard.Cost;
                targetCard.Play();
                Debug.Log("Card added to discard: " + targetCard.Name);
            }
            else
            {
                Debug.Log("Insufficient mana");
            }
        }
        else
        {
            Debug.Log("No target selected");
        }
    }
}
