using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerObstacleContact;
        [SerializeField] private AudioClip playerGatePassed;
        [SerializeField] private AudioClip playerGatePassedPhase2;
        [SerializeField] private AudioClip playerGatePassedPhase3;
        [SerializeField] private AudioClip playerGateDestroyed;

        private void OnEnable()
        {
            EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.AddListener(Events.PLAYER_GATE_PASSED, OnPlayerGatePassed);
            EventManager.AddListener(Events.PLAYER_GATE_DESTROYED, OnPlayerGateDestroyed);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.RemoveListener(Events.PLAYER_GATE_PASSED, OnPlayerGatePassed);
            EventManager.AddListener(Events.PLAYER_GATE_DESTROYED, OnPlayerGateDestroyed);
        }

        private void OnPlayerCollidedObstacle()
        {
            if (playerObstacleContact && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerObstacleContact, targetTransform.position);
        }
        
        private void OnPlayerGatePassed()
        {
            if (!Settings.IsSfxEnabled) return;

            switch (GameState.ComboMultiplier)
            {
                case 0:
                    AudioSource.PlayClipAtPoint(playerGatePassed, targetTransform.position);
                    break;
                case 1:
                    AudioSource.PlayClipAtPoint(playerGatePassed, targetTransform.position);
                    break;
                case 2:
                    AudioSource.PlayClipAtPoint(playerGatePassedPhase2, targetTransform.position);
                    break;
                default:
                    AudioSource.PlayClipAtPoint(playerGatePassedPhase3, targetTransform.position);
                    break;
            }
        }
        
        private void OnPlayerGateDestroyed()
        {
            if (playerGateDestroyed && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerGateDestroyed, targetTransform.position);
        }
    }
}