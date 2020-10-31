using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureEffect : ScriptableObject
{
    public abstract void Activate(ITargetable target = null);
}
