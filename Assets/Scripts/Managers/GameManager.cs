using System.Collections;
using GameAnalyticsSDK;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameSetup gameSetup;
    
    private bool _isGameRunning;

    public static GameManager Instance { get; private set; }
    public GameSetup GameSetup => gameSetup;

    private void Awake()
    {
        Instance = this;
        
        Application.targetFrameRate = 60;
        
        GameState.OnApplicationStarted();
        
        GameSetup.LoadFromFile();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.LEVEL_CHANGED, OnLevelChanged);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Start()
    {
        GameAnalytics.Initialize();
        GameServices.Initialize();
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.LEVEL_CHANGED, OnLevelChanged);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerJumpStarted()
    {
        if (_isGameRunning) return;
        
        _isGameRunning = true;
        
        EventManager.TriggerEvent(Events.GAME_STARTED);
    }

    private void OnGameStarted()
    {
        GameState.OnGameStarted();
    }

    private void OnLevelChanged()
    {
        // if (GameState.Level > 1)
        //     // we completed the previous level
        //     GameAnalytics.NewProgressionEvent(
        //         GAProgressionStatus.Complete, 
        //         "World_1", 
        //         $"Level_{GameState.Level - 1}");
        //
        // GameAnalytics.NewProgressionEvent(
        //     GAProgressionStatus.Start, 
        //     "World_1", 
        //     $"Level_{GameState.Level}");
    }

    private void OnPlayerDied()
    {
        // GameAnalytics.NewProgressionEvent(
        //     GAProgressionStatus.Fail, 
        //     "World_1", 
        //     $"Level_{GameState.Level}",
        //     GameState.Score);
        
        GameServices.ReportScore(Constants.TopScoreLeaderBoardId, GameState.Score);
        GameServices.ReportScore(Constants.TopDistanceReachedId, GameState.Distance);
        GameServices.ReportScore(Constants.TopCitiesDestroyedId, GameState.CitiesDestroyed);
        GameServices.ReportScore(Constants.TopEnemiesDestroyedId, GameState.EnemiesDestroyed);

        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2.5f);
        
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
