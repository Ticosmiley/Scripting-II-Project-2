using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _enemyThinkingTExtUI = null;

    private void OnEnable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void Start()
    {
        _enemyThinkingTExtUI.gameObject.SetActive(false);
    }

    void OnEnemyTurnBegan()
    {
        _enemyThinkingTExtUI.gameObject.SetActive(true);
    }

    void OnEnemyTurnEnded()
    {
        _enemyThinkingTExtUI.gameObject.SetActive(false);
    }
}
