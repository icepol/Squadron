using System;
using System.Collections.Generic;
using UnityEngine;

public class Squadrons : MonoBehaviour
{
    [SerializeField] private int minSpawnedSquadrons = 10;
    
    private List<EnemySquadron> _enemySquadrons;

    private SquadronsSpawner _squadronsSpawner;

    private void Awake()
    {
        _squadronsSpawner = GetComponent<SquadronsSpawner>();
    }

    void Start()
    {
        _enemySquadrons = new List<EnemySquadron>(GetComponentsInChildren<EnemySquadron>());

        SpawnIfNeeded();
    }

    public EnemySquadron GetNextSquadron()
    {
        return _enemySquadrons[0];
    }

    public void DestroySquadron()
    {
        Destroy(_enemySquadrons[0].gameObject);
        
        _enemySquadrons.RemoveAt(0);
        
        SpawnIfNeeded();
    }

    void SpawnIfNeeded()
    {
        while (_enemySquadrons.Count < minSpawnedSquadrons)
        {
            _enemySquadrons.Add(_squadronsSpawner.Spawn());
        }
    }
}
