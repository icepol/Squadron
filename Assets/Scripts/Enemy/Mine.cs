using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem explosion;
    
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;
        
        StartCoroutine(ExplodeAndDestroy());
    }

    IEnumerator ExplodeAndDestroy()
    {
        yield return null;
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
