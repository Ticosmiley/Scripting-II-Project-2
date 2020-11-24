using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamagePlayEffect", menuName = "CardData/PlayEffects/Draw")]
public class DrawPlayEffect : CardEffect
{
    [SerializeField] int _cardsToDraw = 1;

    DeckTester _tester;
    EnemyDeckTester _enemyTester;

    public override void Activate(ITargetable target)
    {
        for (int i = 0; i < _cardsToDraw; i++)
        {
            PlayerTurnCardGameState playerTurn = StateMachine.CurrentState as PlayerTurnCardGameState;
            if (playerTurn != null)
            {
                _tester = FindObjectOfType<DeckTester>();
                _tester.Draw();
            }
            else
            {
                _enemyTester = FindObjectOfType<EnemyDeckTester>();
                _enemyTester.Draw();
            }
        }
    }
}
