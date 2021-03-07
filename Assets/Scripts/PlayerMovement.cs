using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxDistanceDelta = 10f;
    [SerializeField] private float maxDegreesDelta = 1f;

    private PlayerRocket _playerRocket;
    private Squadrons _squadrons;
    private EnemySquadron _targetSquadron;

    private void Awake()
    {
        _playerRocket = GetComponentInChildren<PlayerRocket>();
        _squadrons = FindObjectOfType<Squadrons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_targetSquadron)
            _targetSquadron = _squadrons.GetNextSquadron();
        
        if (!_targetSquadron)
            return;
            
        Vector3 targetPosition = _targetSquadron.transform.position;
        targetPosition.x += _playerRocket.CurrentPositionPercentage * _targetSquadron.Width / 2;

        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPosition, 
            maxDistanceDelta * Time.deltaTime);
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            _targetSquadron.transform.rotation, 
            maxDegreesDelta);

        if (transform.position.z >= _targetSquadron.transform.position.z)
        {
            _squadrons.DestroySquadron();
            _targetSquadron = _squadrons.GetNextSquadron();
        }
    }
}
