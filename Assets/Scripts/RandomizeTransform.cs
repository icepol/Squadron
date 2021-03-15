using UnityEngine;

public class RandomizeTransform : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Quaternion rotation;
    
    void Start()
    {
        transform.Translate(new Vector3(
            Random.Range(-position.x, position.x),
            Random.Range(-position.y, position.y),
            Random.Range(-position.z, position.z)), Space.Self);
        
        transform.Rotate(new Vector3(
            Random.Range(-rotation.x, rotation.x),
            Random.Range(-rotation.y, rotation.y),
            Random.Range(-rotation.z, rotation.z)), Space.Self);
    }
}
