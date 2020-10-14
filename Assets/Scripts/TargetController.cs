using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static ITargetable CurrentTarget;

    [SerializeField] Creature _objectToTarget = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ITargetable possibleTarget = _objectToTarget.GetComponent<ITargetable>();
            if (possibleTarget != null)
            {
                Debug.Log("New target acquired!");
                CurrentTarget = possibleTarget;
                _objectToTarget.Target();
            }
        }
    }
}
