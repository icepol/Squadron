using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSetup", menuName = "Assets/Skin Setup")]
public class SkinSetup : LoadSaveScriptableObject
{
    private const string FILENAME_PREFIX = "skin_setup_";
    private const string FILENAME_POSTFIX = ".json";

    [Header("Basic Setup")]
    public string skinName;
    public int scoreForUnlock;
    
    [SerializeField] private bool isUnlocked;

    public bool IsUnlocked
    {
        get => isUnlocked;
        set
        {
            isUnlocked = value;
            
            if (isPersistent)
                SaveToFile($"{FILENAME_PREFIX}{skinName.ToLower()}{FILENAME_POSTFIX}");
        }
    }
    
    [Header("Visual Setup")]
    public GameObject model;
    
    [Header("Game Services Setup")]
    public string achievementIdAndroid;
    public string achievementIdIos;
    
    [Header("Build setup")]
    public bool isProduction;
    public bool isPersistent;

    private void OnEnable()
    {
        EventManager.AddListener(Events.SCORE_CHANGED, OnScoreChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.SCORE_CHANGED, OnScoreChanged);
    }

    private void OnScoreChanged()
    {
        if (IsUnlocked || GameState.Score < scoreForUnlock) return;

        IsUnlocked = true;
        
#if UNITY_IPHONE
        GameServices.UnlockAchievement(achievementIdIos);
#elif UNITY_ANDROID
        GameServices.UnlockAchievement(achievementIdAndroid);
#else
        // will only log the unlock event
        GameServices.UnlockAchievement(achievementIdIos);
#endif
    }
    
    public void LoadFromFile()
    {
        if (isPersistent)
            LoadFromFile($"{FILENAME_PREFIX}{skinName.ToLower()}{FILENAME_POSTFIX}");
    }

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;

        IsUnlocked = false;
    }
}