using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    AbilityCard _card;
    int _index;

    public AbilityCard Card { get { return _card; } }
    public int Index { get { return _index; } }

    public void Setup(AbilityCard card, int index)
    {
        _card = card;
        _index = index;
    }
}
