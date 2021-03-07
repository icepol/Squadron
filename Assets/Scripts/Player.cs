using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem rocketExplosion;

    private PlayerRocket _playerRocket;

    private void Awake()
    {
        _playerRocket = GetComponentInChildren<PlayerRocket>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
    }

    private void OnDisable()
    {
        EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
    }

    private void OnPlayerCollidedObstacle()
    {
        Instantiate(rocketExplosion, _playerRocket.transform.position, Quaternion.identity);
        
        EventManager.TriggerEvent(Events.PLAYER_DIED);
        
        Destroy(gameObject);
    }
}
