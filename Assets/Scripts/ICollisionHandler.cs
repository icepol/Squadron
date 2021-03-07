using UnityEngine;

public interface ICollisionHandler
{
    void OnTriggerEnter(Collider other);
}