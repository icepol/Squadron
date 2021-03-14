using System;
using pixelook;
using UnityEngine;

public class Gate : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem gatePass;

    private GateBorders _gateBorders;
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
        
        IsFollowing = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;

        EventManager.TriggerEvent(Events.GATE_PASSED);
        
        Instantiate(gatePass, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
