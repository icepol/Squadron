using pixelook;
using UnityEngine;

public class FollowPlayerRotation : MonoBehaviour
{
    private Player _player;
    
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }
    
    private void Start()
    {
        if (_player != null)
            transform.rotation = Quaternion.Euler(0, 0, _player.transform.rotation.eulerAngles.z);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }
    
    private void OnPlayerRotationChanged(Vector3 rotation)
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = rotation.z;
         
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
