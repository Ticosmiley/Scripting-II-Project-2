using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> friendlySpawns = new List<GameObject>();
    [SerializeField] Transform[] _friendlySpawnSlots;
    public bool[] _friendlySlotsFilled = new bool[] { false, false, false, false, false };

    public List<GameObject> enemySpawns = new List<GameObject>();
    [SerializeField] Transform[] _enemySpawnSlots;
    public bool[] _enemySlotsFilled = new bool[] { false, false, false, false, false };

    public List<SpawnView> _friendlySpawnViews = new List<SpawnView>();
    public List<SpawnView> _enemySpawnViews = new List<SpawnView>();
    [SerializeField] SpawnView _spawnView;
    [SerializeField] GameObject _canvas;
    [SerializeField] PlayerTurnCardGameState _playerTurnState;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddSpawn(GameObject spawn, bool isEnemy)
    {
        SpawnView newSView = Instantiate(_spawnView).GetComponent<SpawnView>();
        newSView.transform.SetParent(_canvas.transform, false);
        newSView.Display(spawn.GetComponent<Creature>());

        if (!isEnemy)
        {
            _friendlySpawnViews.Add(newSView);

            for (int i = 0; i < 5; i++)
            {
                if (!_friendlySlotsFilled[i])
                {
                    spawn.transform.position = _friendlySpawnSlots[i].position;
                    spawn.GetComponent<Creature>().boardIndex = i;
                    spawn.GetComponent<Creature>().OnDeath += OnCreatureDeath;
                    friendlySpawns.Add(spawn);
                    _friendlySlotsFilled[i] = true;
                    break;
                }
            }
        }
        else
        {
            _enemySpawnViews.Add(newSView);

            for (int i = 0; i < 5; i++)
            {
                if (!_enemySlotsFilled[i])
                {
                    spawn.transform.position = _enemySpawnSlots[i].position;
                    spawn.GetComponent<Creature>().boardIndex = i;
                    spawn.GetComponent<Creature>().isEnemy = true;
                    spawn.GetComponent<Creature>().OnDeath += OnCreatureDeath;
                    enemySpawns.Add(spawn);
                    _enemySlotsFilled[i] = true;
                    break;
                }
            }
        }
    }

    public void RemoveSpawn(Creature creature)
    {
        if (!creature.isEnemy)
        {
            Destroy(friendlySpawns[creature.boardIndex]);
            friendlySpawns.RemoveAt(creature.boardIndex);
            Destroy(_friendlySpawnViews[creature.boardIndex].gameObject);
            _friendlySpawnViews.RemoveAt(creature.boardIndex);
            _friendlySlotsFilled[creature.boardIndex] = false;

            for (int i = 0; i < friendlySpawns.Count; i++)
            {
                Creature c = friendlySpawns[i].GetComponent<Creature>();
                c.boardIndex = i;
                c.transform.position = _friendlySpawnSlots[i].position;
                _friendlySlotsFilled[i] = true;
            }

            for (int i = 0; i < 5; i++)
            {
                if (i > friendlySpawns.Count - 1)
                    _friendlySlotsFilled[i] = false;
            }
        }
        else
        {
            Destroy(enemySpawns[creature.boardIndex]);
            enemySpawns.RemoveAt(creature.boardIndex);
            Destroy(_enemySpawnViews[creature.boardIndex].gameObject);
            _enemySpawnViews.RemoveAt(creature.boardIndex);
            _enemySlotsFilled[creature.boardIndex] = false;

            for (int i = 0; i < enemySpawns.Count; i++)
            {
                Creature c = enemySpawns[i].GetComponent<Creature>();
                c.boardIndex = i;
                c.transform.position = _enemySpawnSlots[i].position;
                _enemySlotsFilled[i] = true;
            }

            for (int i = 0; i < 5; i++)
            {
                if (i > enemySpawns.Count - 1)
                    _enemySlotsFilled[i] = false;
            }
        }
    }

    public void CreatureAttack(int index)
    {
        TargetController.CurrentTarget = null;
        Creature creature = friendlySpawns[index].GetComponent<Creature>();
        if (creature.canAttack)
        {
            Debug.Log("Select a target");
            _playerTurnState.targeting = true;
            StartCoroutine(CreatureAttackTarget(creature));
        }
        else
            Debug.Log("Creature cannot attack");
    }

    IEnumerator WaitForTarget()
    {
        while (TargetController.CurrentTarget == null)
        {
            yield return null;
        }
    }

    IEnumerator CreatureAttackTarget(Creature creature)
    {
        if (!creature.isEnemy)
        {
            yield return StartCoroutine(WaitForTarget());

            IDamageable target = TargetController.CurrentTarget as IDamageable;
            if (target.IsEnemy())
            {
                target.TakeDamage(creature.Attack);

                Creature enemyCreature = target as Creature;
                if (enemyCreature != null)
                {
                    creature.TakeDamage(enemyCreature.Attack);
                }

                creature.canAttack = false;
            }
            else
                Debug.Log("Invalid target");
        }
        else
        {
            IDamageable target = Player.instance as IDamageable;
            target.TakeDamage(creature.Attack);
            creature.canAttack = false;

            Debug.Log(creature.name + " attacked the player for " + creature.Attack + " damage.");
        }

        _playerTurnState.targeting = false;
    }

    public IEnumerator EnemyAttacks()
    {
        foreach (var creature in enemySpawns)
        {
            if (creature.GetComponent<Creature>().canAttack)
            {
                StartCoroutine(CreatureAttackTarget(creature.GetComponent<Creature>()));
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnCreatureDeath(Creature creature)
    {
        Debug.Log(creature.name + " died");
        RemoveSpawn(creature);
    }
}
