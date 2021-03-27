using pixelook;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquadronsSpawner : MonoBehaviour
{
    [SerializeField] private float startZ = 15;
    [SerializeField] private float offsetZ = 15;
    [SerializeField] private float offsetX = 2;
    [SerializeField] private float rotation = 45;
    [SerializeField] private int directionStepsMin = 1;
    [SerializeField] private int directionStepsMax = 1;

    private float _nextZ;
    private float _lastRotation;
    private float _directionSteps;
    private float _currentRotation;

    private void Awake()
    {
        _nextZ = startZ;
    }

    public EnemySquadron Spawn()
    {
        EnemySquadron[] availableSquadrons = GameManager.Instance.GameSetup.CurrentLevel.enemySquadrons;
        
        EnemySquadron instance = Instantiate(availableSquadrons[Random.Range(0, availableSquadrons.Length)], transform);

        _lastRotation += Random.Range(-rotation, rotation);

        instance.transform.position = new Vector3(Random.Range(-offsetX, offsetX), 0, _nextZ);
        instance.Initialize();
        
        instance.transform.Rotate(Vector3.forward * GetRotation());
        instance.StorePositions();
        
        _nextZ += offsetZ;

        GameState.SpawnedSquadronsCount++;

        return instance;
    }

    private float GetRotation()
    {
        _directionSteps--;
        
        if (_directionSteps <= 0)
        {
            _currentRotation = Random.Range(0, rotation);
            _directionSteps = Random.Range(directionStepsMin, directionStepsMax);
        }

        _lastRotation += _currentRotation;

        return _lastRotation;
    }
}
