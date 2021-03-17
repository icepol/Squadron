using System;
using pixelook;
using UnityEngine;

public class Gate : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem gatePass;

    private GateBorders _gateBorders;
    private Player _player;
    private bool _isFollowing;

    public bool IsFollowing
    {
        get => _isFollowing;
        
        set
        {
            _isFollowing = value;

            _gateBorders.gameObject.SetActive(_isFollowing);
        }
    }

    private void Awake()
    {
        _gateBorders = GetComponentInChildren<GateBorders>();
        _player = FindObjectOfType<Player>();
        
        IsFollowing = false;
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }

    private void Start()
    {
        if (_player != null)
            transform.rotation = Quaternion.Euler(0, 0, _player.transform.rotation.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;

        EventManager.TriggerEvent(Events.PLAYER_GATE_PASSED);
        
        Instantiate(gatePass, transform.position, Quaternion.identity);

        GameState.Score++;
        
        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }
    
    private void OnPlayerRotationChanged(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation.z);
    }
}
