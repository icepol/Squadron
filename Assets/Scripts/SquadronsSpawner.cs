using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquadronsSpawner : MonoBehaviour
{
    [SerializeField] private EnemySquadron[] squadronPrefabs;
    
    [SerializeField] private float startZ = 15;
    [SerializeField] private float offsetZ = 15;
    [SerializeField] private float offsetX = 2;
    [SerializeField] private float rotation = 45;

    private float _nextZ;

    private void Awake()
    {
        _nextZ = startZ;
    }

    public EnemySquadron Spawn()
    {
        EnemySquadron instance = Instantiate(squadronPrefabs[Random.Range(0, squadronPrefabs.Length)], transform);

        instance.transform.position = new Vector3(Random.Range(-offsetX, offsetX), 0, _nextZ);
        instance.transform.Rotate(Vector3.forward * Random.Range(-rotation, rotation));

        _nextZ += offsetZ;

        return instance;
    }
}
