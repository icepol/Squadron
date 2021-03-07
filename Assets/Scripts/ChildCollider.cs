using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    private ICollisionHandler[] _collisionHandlers;

    private void Awake()
    {
        _collisionHandlers = GetComponentsInParent<ICollisionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (ICollisionHandler collisionHandler in _collisionHandlers)
        {
            collisionHandler.OnTriggerEnter(other);            
        }
    }
}
