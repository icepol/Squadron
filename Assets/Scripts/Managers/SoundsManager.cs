using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerObstacleContact;
        [SerializeField] private AudioClip playerGatePassed;

        private void OnEnable()
        {
            EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.AddListener(Events.PLAYER_GATE_PASSED, OnPlayerGatePassed);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.RemoveListener(Events.PLAYER_GATE_PASSED, OnPlayerGatePassed);
        }

        private void OnPlayerCollidedObstacle()
        {
            if (playerObstacleContact && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerObstacleContact, targetTransform.position);
        }
        
        private void OnPlayerGatePassed()
        {
            if (playerGatePassed && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerGatePassed, targetTransform.position);
        }
    }
}