using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySquadron : MonoBehaviour
{
    [SerializeField] private Gate gatePrefab; 
    
    private List<Enemy> _enemies;
    private IFollowing[] _followings;

    private bool _isFollowing;
    private Transform _mostLeftElement;
    private Transform _mostRightElement;

    public Vector3 LeftPosition { get; private set; }
    public Vector3 RightPosition { get; private set; }

    public bool IsFollowing
    {
        get => _isFollowing;
        
        set
        {
            _isFollowing = value;

            foreach (IFollowing following in _followings)
            {
                following.IsFollowing = _isFollowing;
            }
        }
    }

    private void Awake()
    {
        _enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
    }

    public void Initialize()
    {
        // called after the squadron is on the right position
        
        CalculatePositions();
        AddGate();
        AddEnemy();
        AddCity();
        
        _followings = GetComponentsInChildren<IFollowing>();
    }

    private void CalculatePositions()
    {
        float minX = _enemies[0].transform.position.x;
        float maxX = minX;

        foreach (Enemy enemy in _enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;

            if (enemyPosition.x < minX)
            {
                minX = enemyPosition.x;
                _mostLeftElement = enemy.transform;
            }

            if (enemyPosition.x > maxX)
            {
                maxX = enemyPosition.x;
                _mostRightElement = enemy.transform;
            }
        }
    }

    public void StorePositions()
    {
        LeftPosition = _mostLeftElement.position;
        RightPosition = _mostRightElement.position;
    }

    private void AddGate()
    {
        int enemyOffset = Random.Range(0, _enemies.Count);
        Vector3 position = _enemies[enemyOffset].transform.position;
        
        Destroy(_enemies[enemyOffset].gameObject);
        _enemies.RemoveAt(enemyOffset);
        
        Instantiate(gatePrefab, position, Quaternion.identity, transform);
    }

    private void AddEnemy()
    {
        if (GameState.SpawnedSquadronsCount < GameManager.Instance.GameSetup.minSquadronCountToSpawnEnemies) return;
        
        for (int i = 0; i < GameManager.Instance.GameSetup.CurrentLevel.enemiesInSquadron; i++)
        {
            if (!(Random.Range(0f, 1f) < GameManager.Instance.GameSetup.CurrentLevel.enemyRatio)) continue;
            
            int enemyOffset = Random.Range(0, _enemies.Count);
            Vector3 position = _enemies[enemyOffset].transform.position;
                
            Destroy(_enemies[enemyOffset].gameObject);

            Enemy enemyToSpawn =
                GameManager.Instance.GameSetup.CurrentLevel.enemies[
                    Random.Range(0, GameManager.Instance.GameSetup.CurrentLevel.enemies.Length)];
                
            _enemies[enemyOffset] = Instantiate(enemyToSpawn, position, Quaternion.identity, transform);
        }
    }

    private void AddCity()
    {
        if (GameState.SpawnedSquadronsCount < GameManager.Instance.GameSetup.minSquadronCountToSpawnCities) return;

        if (!(Random.Range(0f, 1f) < GameManager.Instance.GameSetup.CurrentLevel.enemyCityRatio)) return;
        
        Vector3 position;
            
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            position  = _mostLeftElement.position + Vector3.left * 4;
        }
        else
        {
            position  = _mostRightElement.position + Vector3.right * 4;
        }

        EnemyCity cityToSpawn =
            GameManager.Instance.GameSetup.CurrentLevel.enemyCities[
                Random.Range(0, GameManager.Instance.GameSetup.CurrentLevel.enemyCities.Length)];

        Instantiate(cityToSpawn, position, Quaternion.identity, transform);
    }

    public void Remove()
    {
        StartCoroutine(WaitAndRemove());
    }

    private IEnumerator WaitAndRemove()
    {
        GameState.Distance++;
        
        yield return new WaitForSeconds(1.5f);
        
        Destroy(gameObject);
    }
}
