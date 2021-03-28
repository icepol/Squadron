using pixelook;
using UnityEngine;

public class GateBorders : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem gateExplosion;
    
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;
        
        Instantiate(gateExplosion, transform.position, Quaternion.identity);
        
        EventManager.TriggerEvent(Events.PLAYER_GATE_DESTROYED);
        
        Destroy(gameObject);
    }
}
