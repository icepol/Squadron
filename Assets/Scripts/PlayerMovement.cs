using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxDistanceDelta = 10f;

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

    void Update()
    {
        if (!_targetSquadron)
            _targetSquadron = GetNextSquadron();

        if (!_targetSquadron)  return;
            
        MoveToPosition();
        
        RotateToPosition();

        CheckTargetPosition();
    }

    private EnemySquadron GetNextSquadron()
    {
        _targetSquadron = _squadrons.GetNextSquadron();

        _currentRotationTime = 0;
        _timeToRotate = Vector3.Distance(transform.position, _targetSquadron.transform.position) / maxDistanceDelta *
                        0.75f;

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
        Vector3 targetPosition = _targetSquadron.transform.position;
        targetPosition.x += _playerRocket.CurrentPositionPercentage * _targetSquadron.Width / 2;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            maxDistanceDelta * Time.deltaTime);
    }

    private void RotateToPosition()
    {
        if (!(_currentRotationTime < _timeToRotate)) return;
        
        _currentRotationTime += Time.deltaTime;

        transform.rotation = Quaternion.Lerp(_sourceRotation, _targetRotation, _currentRotationTime / _timeToRotate);
    }
}
