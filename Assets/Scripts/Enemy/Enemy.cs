using System.Collections;
using pixelook;
using UnityEngine;

public class Enemy : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ScoreBalloon scoreBalloonPrefab;
    [SerializeField] private bool destroyAfterCollision;
    [SerializeField] private int destroyScore = 15;
    
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        if (player == null) return;
        
        if (destroyAfterCollision || player.IsImmortal)
            StartCoroutine(ExplodeAndDestroy(player.IsImmortal, GameState.ComboMultiplier));
        
        EventManager.TriggerEvent(Events.PLAYER_COLLIDED_OBSTACLE);
    }
    
    IEnumerator ExplodeAndDestroy(bool isImmortal, int comboMultiplier)
    {
        yield return null;

        if (isImmortal)
        {
            int score = destroyScore * comboMultiplier;
        
            GameState.Score += score;
        
            ShowScore(score, transform.position);
        }
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
    
    private void ShowScore(int score, Vector3 gatePassPosition)
    {
        Vector3 scoreBalloonPosition = gatePassPosition;
        scoreBalloonPosition.z += 4;

        ScoreBalloon scoreBalloon = Instantiate(scoreBalloonPrefab, scoreBalloonPosition, Quaternion.identity);
        scoreBalloon.SetScore(score);
    }
}
