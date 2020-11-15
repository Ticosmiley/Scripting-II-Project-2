using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeckTester : MonoBehaviour
{
    public List<AbilityCardData> _abilityDeckConfig = new List<AbilityCardData>();

    Deck<AbilityCard> _abilityDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityDiscard = new Deck<AbilityCard>();
    Deck<AbilityCard> _enemyHand = new Deck<AbilityCard>();

    [SerializeField] SpawnManager _spawnManager;

    public void SetupAbilityDeck()
    {
        _enemyHand.Clear();
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
        Debug.Log("Enemy drew card: " + newCard.Name);
        if (_enemyHand.Count < 5)
        {
            _enemyHand.Add(newCard, DeckPosition.Top);
        }
        else
        {
            _abilityDiscard.Add(newCard, DeckPosition.Top);
            Debug.Log("Enemy hand full. Discarded " + newCard.Name);
        }
    }

    public IEnumerator PlayCards()
    {
        for (int i = 0; i < _enemyHand.Count; i++)
        {
            AbilityCard targetCard = _enemyHand.GetCard(i);
            if (targetCard.Cost <= Opponent.instance.currentMana)
            {
                SpawnPlayEffect spawnEffect = targetCard.CardEffect as SpawnPlayEffect;
                if (spawnEffect != null)
                {
                    if (_spawnManager.enemySpawns.Count < 5)
                    {
                        _enemyHand.Remove(i);
                        _abilityDiscard.Add(targetCard);
                        Opponent.instance.currentMana -= targetCard.Cost;
                        TargetController.CurrentTarget = FindObjectOfType<EnemyTable>() as ITargetable;
                        targetCard.Play();
                        Debug.Log("Enemy played " + targetCard.Name);
                    }
                }

                DamagePlayEffect damageEffect = targetCard.CardEffect as DamagePlayEffect;
                if (damageEffect != null)
                {
                    _enemyHand.Remove(i);
                    _abilityDiscard.Add(targetCard);
                    Opponent.instance.currentMana -= targetCard.Cost;
                    TargetController.CurrentTarget = Player.instance as ITargetable;
                    targetCard.Play();
                    Debug.Log("Enemy played " + targetCard.Name);
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
