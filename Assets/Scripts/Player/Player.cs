using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem rocketExplosion;
    
    private PlayerRocket _playerRocket;
    
    public bool IsImmortal { get; private set; }

    private void Awake()
    {
        _playerRocket = GetComponentInChildren<PlayerRocket>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
        EventManager.AddListener(Events.COMBO_MULTIPLIER_CHANGED, OnComboMultiplierChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
        EventManager.RemoveListener(Events.COMBO_MULTIPLIER_CHANGED, OnComboMultiplierChanged);
    }

    private void OnComboMultiplierChanged()
    {
        IsImmortal = GameState.ComboMultiplier >= 3;
    }

    private void OnPlayerCollidedObstacle()
    {
        if (IsImmortal)
        {
            GameState.ComboMultiplier = 0;
        }
        else
        {
            Instantiate(rocketExplosion, _playerRocket.transform.position, Quaternion.identity);
        
            EventManager.TriggerEvent(Events.PLAYER_DIED);
        
            Destroy(gameObject);            
        }
    }
}
