using System;
using pixelook;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float referenceWidth = 1242f;
    [SerializeField] private float mouseSensitivity = 3f;
 
    private Vector3 _startRocketPosition;
    private Vector3 _startMousePosition;
    private float _controllerHeight;
    private float _mouseSensitivity;

    private bool _isEnabled = true;
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
        EventManager.AddListener(Events.PANEL_SHOW, OnPanelShow);
        EventManager.AddListener(Events.PANEL_HIDE, OnPanelHide);
    }

    private void Start()
    {
        _controllerHeight = Screen.height / 3f;
        _mouseSensitivity = (Screen.width / referenceWidth) * mouseSensitivity;
    }

    void Update()
    {
        MovePlayerRocker();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PANEL_SHOW, OnPanelShow);
        EventManager.RemoveListener(Events.PANEL_HIDE, OnPanelHide);
    }

    private void OnPanelShow()
    {
        _isEnabled = false;
    }

    private void OnPanelHide()
    {
        _isEnabled = true;
    }

    private void MovePlayerRocker()
    {
        if (!_isEnabled) return;
        
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < _controllerHeight)
        {
            if (!_isGameRunning)
            {
                _isGameRunning = true;
                EventManager.TriggerEvent(Events.GAME_STARTED);
            }
            
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
                                         (Input.mousePosition.x - _startMousePosition.x) / Screen.width * _mouseSensitivity);
        }
        else if (_isGameRunning && _isReturning)
        {
            _playerRocket.ReturnToBasePosition();
        }
    }
}
