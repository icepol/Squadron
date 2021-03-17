using pixelook;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        transform.parent = null;
    }
}
