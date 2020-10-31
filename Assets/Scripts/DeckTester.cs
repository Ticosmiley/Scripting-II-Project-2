using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour
{
    [SerializeField] List<AbilityCardData> _abilityDeckConfig = new List<AbilityCardData>();

    Deck<AbilityCard> _abilityDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityDiscard = new Deck<AbilityCard>();

    Deck<AbilityCard> _playerHand = new Deck<AbilityCard>();

    [SerializeField] PlayerHandView _playerHandView;

    public Deck<AbilityCard> PlayerHand { get { return _playerHand; } }
    public Deck<AbilityCard> AbilityDeck { get { return _abilityDeck; } }
    public Deck<AbilityCard> AbilityDiscard { get { return _abilityDiscard; } }

    private void Start()
    {
        SetupAbilityDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Draw();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrintPlayerHand();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayCard();
        }
    }

    private void SetupAbilityDeck()
    {
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

            _playerHandView.Display(newCard, _playerHand.Count - 1);
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
        AbilityCard targetCard = _playerHand.GetCard(_playerHandView.CurrentCardIndex);
        targetCard.Play();
        _playerHand.Remove(_playerHandView.CurrentCardIndex);
        _abilityDiscard.Add(targetCard);
        _playerHandView.Next();
        Debug.Log("Card added to discard: " + targetCard.Name);
    }
}
