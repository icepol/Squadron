using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem rocketExplosion;
    
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
        Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        
        EventManager.TriggerEvent(Events.PLAYER_DIED);
        
        Destroy(gameObject);
    }
}
