using System;
using pixelook;
using UnityEngine;

public class EnemyAircraft : MonoBehaviour, IFollowing
{
    [SerializeField] private ParticleSystem engineDust;
    [SerializeField] private ParticleSystem explosion;

    [SerializeField] private bool isTriggeredByDistance;
    [SerializeField] private float triggerDistance;
    [SerializeField] private float stopChasingAtDistance;
    
    [SerializeField] private bool isTriggeredWhenFollowing;

    [SerializeField] private float chaseSpeed = 1;
    [SerializeField] private bool chaseConstantly;

    private bool _isFollowing;
    private bool _isTriggered;

    private Vector3 _targetPosition;
    
    public bool IsFollowing
    {
        get => _isFollowing;
        
        set
        {
            _isFollowing = value;

            if (isTriggeredWhenFollowing && _isFollowing)
                TriggerChasing();
        }
    }
    
    void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_POSITION_CHANGED, OnPlayerPositionChanged);
    }

    private void Start()
    {
        engineDust.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    private void OnDisable()
    {
        EventManager.AddListener(Events.PLAYER_POSITION_CHANGED, OnPlayerPositionChanged);
    }

    private void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        if (_isTriggered && !chaseConstantly) return;
        
        // enemy will fly away if player already passed the enemy 
        _targetPosition = transform.position.z - playerPosition.z > stopChasingAtDistance
            ? playerPosition
            : new Vector3(transform.position.x, transform.position.y, 0);

        if (!_isTriggered && isTriggeredByDistance && Vector3.Distance(transform.position, _targetPosition) < triggerDistance)
            TriggerChasing();
    }

    void TriggerChasing()
    {
        _isTriggered = true;
        
        if (engineDust != null)
            engineDust.Play();
    }

    void ChasePlayer()
    {
        if (!_isTriggered) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, chaseSpeed * Time.deltaTime);
    }
}
