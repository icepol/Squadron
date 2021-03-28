using pixelook;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxDistanceDelta = 10f;

    private bool _isGameRunning;
    private PlayerRocket _playerRocket;
    private Squadrons _squadrons;
    private EnemySquadron _targetSquadron;
    
    private float _timeToRotate;
    private float _currentRotationTime;
    private Quaternion _sourceRotation;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _playerRocket = GetComponentInChildren<PlayerRocket>();
        _squadrons = FindObjectOfType<Squadrons>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
    }

    void Update()
    {
        if (!_isGameRunning) return;
        
        if (!_targetSquadron)
            _targetSquadron = GetNextSquadron();

        if (!_targetSquadron)  return;
            
        MoveToPosition();
        
        RotateToPosition();

        CheckTargetPosition();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        _isGameRunning = true;
    }

    private EnemySquadron GetNextSquadron()
    {
        _targetSquadron = _squadrons.GetFollowingSquadron();

        _currentRotationTime = 0;
        _timeToRotate = Vector3.Distance(transform.position, _targetSquadron.transform.position) / GetMaxDistanceDelta() *
                        0.7f;

        _sourceRotation = transform.rotation;
        _targetRotation = _targetSquadron.transform.rotation;
        
        return _targetSquadron;
    }

    private void CheckTargetPosition()
    {
        if (!(transform.position.z >= _targetSquadron.transform.position.z)) return;
        
        _squadrons.DestroySquadron();
        _targetSquadron = null;
    }

    private void MoveToPosition()
    {
        Vector3 targetPosition = Vector3.Lerp(_targetSquadron.LeftPosition, _targetSquadron.RightPosition, _playerRocket.CurrentPositionPercentage);
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            GetMaxDistanceDelta() * Time.deltaTime);
    }

    private void RotateToPosition()
    {
        if (!(_currentRotationTime < _timeToRotate)) return;
        
        _currentRotationTime += Time.deltaTime;

        transform.rotation = Quaternion.Lerp(_sourceRotation, _targetRotation, _currentRotationTime / _timeToRotate);
        
        EventManager.TriggerEvent(Events.PLAYER_ROTATION_CHANGED, transform.rotation.eulerAngles);
        EventManager.TriggerEvent(Events.PLAYER_POSITION_CHANGED, transform.position);
    }

    private float GetMaxDistanceDelta()
    {
        return GameState.ComboMultiplier switch
        {
            0 => maxDistanceDelta,
            1 => maxDistanceDelta,
            2 => maxDistanceDelta * 1.1f,
            _ => maxDistanceDelta * 1.3f
        };
    }
}
