using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private RequiredForUnlock requiredForUnlockPrefab;
    [SerializeField] private float moveTime = 0.5f;
    
    private Animator _animator;
    private SkinModelWrapper _skinModelWrapper;

    private bool _isMoving;
    private float _currentMoveTime;
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _skinModelWrapper = GetComponentInChildren<SkinModelWrapper>();
    }

    private void Update()
    {
        Move();
    }

    public void Setup(SkinSetup skinSetup, float animationOffset)
    {
        if (skinSetup.IsUnlocked || GameManager.Instance.GameSetup.AreUnlockedAll)
        {
            Instantiate(skinSetup.model, _skinModelWrapper.transform, false);
        
            _skinModelWrapper.transform.Rotate(0, 180, 0);
            
            _animator.enabled = true;
            _animator.Play(
                _animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
                0,
                animationOffset);
        }
        else
        {
            RequiredForUnlock instance = Instantiate(requiredForUnlockPrefab, _skinModelWrapper.transform, false);
            instance.SetRequiredValue(skinSetup.scoreForUnlock);
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _sourcePosition = transform.localPosition;
        _targetPosition = targetPosition;

        _currentMoveTime = 0;
        
        _isMoving = true;
    }

    private void Move()
    {
        if (!_isMoving) return;
        
        _currentMoveTime += Time.deltaTime;
        
        transform.localPosition = Vector3.Lerp(_sourcePosition, _targetPosition, _currentMoveTime / moveTime);
     
        if (_currentMoveTime < moveTime) return;

        _isMoving = false;
    }
}