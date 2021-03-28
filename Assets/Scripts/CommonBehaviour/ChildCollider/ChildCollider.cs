using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    [SerializeField] private GameObject[] collisionHandlers;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject collisionHandler in collisionHandlers)
        {
            collisionHandler.GetComponent<ICollisionHandler>().OnTriggerEnter(other);            
        }
    }
}
