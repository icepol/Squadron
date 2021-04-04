using pixelook;
using UnityEngine;

public class EnemyCity : MonoBehaviour, ICollisionHandler
{
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;

        GameState.CitiesDestroyed++;
    }
}
