using pixelook;
using UnityEngine;

public class GateArrows : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_ROTATION_CHANGED, OnPlayerRotationChanged);
    }

    private void OnPlayerRotationChanged(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation.z);
    }
}
