using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : Card
{
    public int Cost { get; private set; }
    public Sprite Graphic { get; private set; }
    public CardEffect CardEffect { get; private set; }
    public string Description { get; private set; }
    public bool Played { get; set; } = false;

    public AbilityCard(AbilityCardData Data)
    {
        Name = Data.Name;
        Cost = Data.Cost;
        Graphic = Data.Graphic;
        CardEffect = Data.CardEffect;
        Description = Data.Description;
    }

    public override void Play()
    {
        ITargetable target = TargetController.CurrentTarget;
        Debug.Log("Playing ability card: " + Name);
        CardEffect.Activate(target);
    }
}
