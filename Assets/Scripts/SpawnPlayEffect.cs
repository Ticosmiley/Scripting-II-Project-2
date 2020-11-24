using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnPlayEffect", menuName = "CardData/PlayEffects/Spawn")]
public class SpawnPlayEffect : CardEffect
{
    [SerializeField] GameObject _prefabToSpawn = null;

    public override void Activate(ITargetable target)
    {
        MonoBehaviour worldObject = target as MonoBehaviour;

        if (worldObject != null)
        {
            Vector3 spawnLocation;
            PlayerTurnCardGameState playerTurn = StateMachine.CurrentState as PlayerTurnCardGameState;
            if (playerTurn != null)
            {
                spawnLocation = Player.instance.transform.position;
                GameObject newGameObject = Instantiate(_prefabToSpawn, spawnLocation, Quaternion.identity);
                SpawnManager.instance.AddSpawn(newGameObject, false);
            }
            else
            {
                spawnLocation = Opponent.instance.transform.position;
                GameObject newGameObject = Instantiate(_prefabToSpawn, spawnLocation, Quaternion.identity);
                SpawnManager.instance.AddSpawn(newGameObject, true);
            }
        }
        else
        {
            Debug.Log("Target does not have a transform...");
        }
    }
}
