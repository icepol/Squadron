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

    public EnemySquadron GetFollowingSquadron()
    {
        _enemySquadrons[0].IsFollowing = true;
        
        return _enemySquadrons[0];
    }

    public void DestroySquadron()
    {
        EnemySquadron enemySquadron = _enemySquadrons[0];
        _enemySquadrons.RemoveAt(0);

        enemySquadron.Remove();
        
        SpawnIfNeeded();
    }

    void SpawnIfNeeded()
    {
        while (_enemySquadrons.Count < GameManager.Instance.GameSetup.minSpawnedSquadrons)
        {
            _enemySquadrons.Add(_squadronsSpawner.Spawn());
        }
    }
}
