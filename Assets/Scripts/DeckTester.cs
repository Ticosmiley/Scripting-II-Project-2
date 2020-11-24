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
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] PlayerTurnCardGameState _playerTurnState;
    [SerializeField] AudioSource _drawSound;
    [SerializeField] AudioSource _playSound;

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
        _drawSound.Play();

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
        newCard.Played = false;
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
        TargetController.CurrentTarget = null;
        AbilityCard targetCard = _playerHand.GetCard(currentCard);
        if (targetCard.Cost <= Player.instance.currentMana)
        {
            Debug.Log("Select a target");
            _playerTurnState.targeting = true;
            DamagePlayEffect damageEffect = targetCard.CardEffect as DamagePlayEffect;
            if (damageEffect != null)
            {
                _playerTurnState.targetingAttack = true;
            }
            StartCoroutine(PlayCardOnTarget(targetCard));
        }
        else
        {
            Debug.Log("Insufficient mana");
        }
    }

    IEnumerator WaitForTarget()
    {
        while(TargetController.CurrentTarget == null)
        {
            yield return null;
        }
    }

    IEnumerator PlayCardOnTarget(AbilityCard card)
    {
        yield return StartCoroutine(WaitForTarget());

        SpawnPlayEffect spawnEffect = card.CardEffect as SpawnPlayEffect;
        if (spawnEffect != null)
        {
            if (_spawnManager.friendlySpawns.Count < 5)
            {
                PlayerTable pTable = TargetController.CurrentTarget as PlayerTable;
                if (pTable != null)
                {
                    card.Played = true;
                    _playSound.Play();
                    _playerHand.Remove(currentCard);
                    StartCoroutine(_playerHandView.RemoveCard(currentCard));
                    _abilityDiscard.Add(card);
                    Player.instance.currentMana -= card.Cost;
                    card.Play();
                    Debug.Log("Card added to discard: " + card.Name);
                }
                else
                    Debug.Log("Invalid target");
            }
            else
                Debug.Log("Board is full.");
        }

        DamagePlayEffect damageEffect = card.CardEffect as DamagePlayEffect;
        if (damageEffect != null)
        {
            IDamageable enemy = TargetController.CurrentTarget as IDamageable;
            if (enemy != null)
            {
                card.Played = true;
                _playSound.Play();
                _playerHand.Remove(currentCard);
                StartCoroutine(_playerHandView.RemoveCard(currentCard));
                _abilityDiscard.Add(card);
                Player.instance.currentMana -= card.Cost;
                card.Play();
                Debug.Log("Card added to discard: " + card.Name);
            }
            else
                Debug.Log("Invalid target");
        }

        DrawPlayEffect drawEffect = card.CardEffect as DrawPlayEffect;
        if (drawEffect != null)
        {
            card.Played = true;
            _playSound.Play();
            _playerHand.Remove(currentCard);
            yield return StartCoroutine(_playerHandView.RemoveCard(currentCard));
            _abilityDiscard.Add(card);
            Player.instance.currentMana -= card.Cost;
            card.Play();
            Debug.Log("Card added to discard: " + card.Name);
        }
        _playerTurnState.targeting = false;
        _playerTurnState.targetingAttack = false;
    }
}
