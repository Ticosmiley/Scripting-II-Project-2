using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawPile : MonoBehaviour
{
    [SerializeField] DeckTester _tester;

    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = _tester.AbilityDeck.Count.ToString();
    }
}
