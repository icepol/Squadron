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
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _animator.SetTrigger("ShakeBig");
        }
    }
}