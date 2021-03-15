using pixelook;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.SCORE_CHANGED, OnScoreChanged);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.SCORE_CHANGED, OnScoreChanged);
    }
    
    private void OnScoreChanged()
    {
        _animator.SetTrigger("ScoreChanged");
    }
}
