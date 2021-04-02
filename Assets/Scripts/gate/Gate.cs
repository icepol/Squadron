using pixelook;
using UnityEngine;

public class Gate : MonoBehaviour, ICollisionHandler, IFollowing
{
    [SerializeField] private ParticleSystem gatePass;
    [SerializeField] private ScoreBalloon scoreBalloonPrefab;

    private GateBorders _gateBorders;
    private bool _isFollowing;

    public bool IsFollowing
    {
        get => _isFollowing;
        
        set
        {
            _isFollowing = value;

            _gateBorders.gameObject.SetActive(_isFollowing);
        }
    }

    private void Awake()
    {
        _gateBorders = GetComponentInChildren<GateBorders>();

        IsFollowing = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player == null) return;

        if (_gateBorders != null)
            CleanGatePass(other.transform.position);
        else
            DirtyGatePass(other.transform.position);
        
        Destroy(gameObject);
    }

    private void CleanGatePass(Vector3 gatePassPosition)
    {
        Instantiate(gatePass, transform.position, Quaternion.identity);
        
        GameState.ComboMultiplier++;

        int score = 10 * GameState.ComboMultiplier;
        
        GameState.Score += score;
        
        ShowScore(score, gatePassPosition);
        
        EventManager.TriggerEvent(Events.PLAYER_GATE_PASSED);
    }

    private void DirtyGatePass(Vector3 gatePassPosition)
    {
        GameState.Score++;
        GameState.ComboMultiplier = 0;
        
        ShowScore(1, gatePassPosition);
    }

    private void ShowScore(int score, Vector3 gatePassPosition)
    {
        Vector3 scoreBalloonPosition = gatePassPosition;
        scoreBalloonPosition.z += 3;

        ScoreBalloon scoreBalloon = Instantiate(scoreBalloonPrefab, scoreBalloonPosition, Quaternion.identity);
        scoreBalloon.SetScore(score);
    }
}
