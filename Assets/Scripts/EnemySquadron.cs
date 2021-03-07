using System.Collections;
using UnityEngine;

public class EnemySquadron : MonoBehaviour
{
    [SerializeField] private Gate gatePrefab; 
    
    private Enemy[] _enemies;
    
    public float Width { get; private set; }

    private void Start()
    {
        _enemies = GetComponentsInChildren<Enemy>();
        
        CalculateWidth();
        AddGate();
    }

    private void CalculateWidth()
    {
        float minX = 0;
        float maxX = 0;

        foreach (Enemy enemy in _enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;
            
            minX = enemyPosition.x < minX ? enemyPosition.x : minX;
            maxX = enemyPosition.x > maxX ? enemyPosition.x : maxX;
        }

        Width = maxX - minX;
    }

    private void AddGate()
    {
        int enemyOffset = Random.Range(0, _enemies.Length);
        Vector3 position = _enemies[enemyOffset].transform.position;
        
        Destroy(_enemies[enemyOffset].gameObject);

        Instantiate(gatePrefab, position, Quaternion.identity, transform);
    }

    public void Remove()
    {
        StartCoroutine(WaitAndRemove());
    }

    private IEnumerator WaitAndRemove()
    {
        yield return new WaitForSeconds(1.5f);
        
        Destroy(gameObject);
    }
}
