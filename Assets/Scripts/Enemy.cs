using pixelook;
using UnityEngine;

public class Enemy : MonoBehaviour, ICollisionHandler
{
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;
        
        EventManager.TriggerEvent(Events.PLAYER_COLLIDED_OBSTACLE);
    }
}
