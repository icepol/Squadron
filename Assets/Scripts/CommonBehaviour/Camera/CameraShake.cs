using UnityEngine;

namespace pixelook
{
    public class CameraShake : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.AddListener(Events.PLAYER_GATE_DESTROYED, OnPlayerGateDestroyed);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.RemoveListener(Events.PLAYER_GATE_DESTROYED, OnPlayerGateDestroyed);
        }

        private void OnPlayerGateDestroyed()
        {
            _animator.SetTrigger("ShakeSmall");
        }

        private void OnPlayerDied()
        {
            _animator.SetTrigger("ShakeBig");
        }
    }
}