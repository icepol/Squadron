using System.Collections;
using UnityEngine;

public class EnemySquadron : MonoBehaviour
{
    [SerializeField] private Gate gatePrefab; 
    
    private Enemy[] _enemies;
    private Mine[] _mines;
    private IFollowing[] _followings;

    private bool _isFollowing;
    
    public float Width { get; private set; }

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
        _enemies = GetComponentsInChildren<Enemy>();
        _mines = GetComponentsInChildren<Mine>();
        
        CalculateWidth();
        AddGate();
        
        _followings = GetComponentsInChildren<IFollowing>();
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
        int enemyOffset = Random.Range(0, _mines.Length);
        Vector3 position = _enemies[enemyOffset].transform.position;
        
        Destroy(_mines[enemyOffset].gameObject);

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
