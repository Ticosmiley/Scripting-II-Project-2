using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> _friendlySpawns = new List<GameObject>();
    [SerializeField] Transform[] _friendlySpawnSlots;
    public bool[] _slotsFilled = new bool[] { false, false, false, false, false };

    public List<SpawnView> _spawnViews = new List<SpawnView>();
    [SerializeField] SpawnView _spawnView;
    [SerializeField] GameObject _canvas;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddSpawn(GameObject spawn)
    {
        SpawnView newSView = Instantiate(_spawnView).GetComponent<SpawnView>();
        newSView.transform.SetParent(_canvas.transform, false);
        newSView.Display(spawn.GetComponent<Creature>());
        _spawnViews.Add(newSView);

        for (int i = 0; i < 5; i++)
        {
            if (!_slotsFilled[i])
            {
                spawn.transform.position = _friendlySpawnSlots[i].position;
                _friendlySpawns.Add(spawn);
                _slotsFilled[i] = true;
                break;
            }
        }
    }
}
