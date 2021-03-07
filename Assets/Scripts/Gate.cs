using UnityEngine;

public class Gate : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem gatePass;
    
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;

        Instantiate(gatePass, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
