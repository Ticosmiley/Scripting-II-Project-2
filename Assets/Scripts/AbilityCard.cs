using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : Card
{
    public int Cost { get; private set; }
    public Sprite Graphic { get; private set; }
    public CardEffect CardEffect { get; private set; }

    public AbilityCard(AbilityCardData Data)
    {
        Name = Data.Name;
        Cost = Data.Cost;
        Graphic = Data.Graphic;
        CardEffect = Data.CardEffect;
    }

    public override void Play()
    {
        ITargetable target = TargetController.CurrentTarget;
        Debug.Log("Playing ability card: " + Name);
        CardEffect.Activate(target);
    }
}
