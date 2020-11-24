using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTable : MonoBehaviour, ITargetable
{
    public void Target()
    {
        TargetController.CurrentTarget = this as ITargetable;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
