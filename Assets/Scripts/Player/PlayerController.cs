using pixelook;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 3f;
 
    private Vector3 _startRocketPosition;
    private Vector3 _startMousePosition;
    private bool _isGameRunning;
    private bool _isMoving;
    private bool _isReturning;
    
    private PlayerRocket _playerRocket;

    private void Awake()
    {
        _playerRocket = GetComponentInChildren<PlayerRocket>();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
    }

    void Update()
    {
        if (!_isGameRunning) return;

        MovePlayerRocker();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        _isGameRunning = true;
    }
    
    private void MovePlayerRocker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Input.mousePosition;
            _startRocketPosition = _playerRocket.transform.localPosition;

            _isMoving = true;
            _isReturning = false;
        }
        else if (Input.GetMouseButtonUp(0) && _isMoving)
        {
            _isMoving = false;
            _isReturning = true;
        }
        else if (Input.GetMouseButton(0) && _isMoving)
        {
            _playerRocket.MoveToPosition(_startRocketPosition.x +
                                         (Input.mousePosition.x - _startMousePosition.x) / Screen.width * mouseSensitivity);
        }
        else if (_isReturning)
        {
            _playerRocket.ReturnToBasePosition();
        }
    }
}
