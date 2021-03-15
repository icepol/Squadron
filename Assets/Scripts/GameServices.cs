using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameServices : MonoBehaviour {

    static bool isInitialized;
    static bool isAuthenticated;
    static bool showAuthentication = true;

    public static void Initialize() {
        if (isInitialized)
            return;

#if UNITY_ANDROID
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.DebugLogEnabled = true;
#endif

        if (showAuthentication) {
            showAuthentication = false;
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

        isInitialized = true;
    }

    public static void ShowLeaderBoard() {
        if (!isAuthenticated) {
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

        Social.ShowLeaderboardUI();
    }

    public static void ReportScore(string boardId, int score) {
        if (Application.isEditor) {
            Debug.Log($"GameServices: Report score {score} to board {boardId}");
            return;
        }

        if (isAuthenticated) {
            Social.ReportScore(
                score, boardId, success => { }
            );
        }
    }
    
    public static void UnlockAchievement(string achievementId) {
        if (Application.isEditor) {
            Debug.Log($"GameServices: UnlockAchievement {achievementId}");
            return;
        }

        Social.ReportProgress(achievementId, 100f, (bool success) => {
            Debug.Log($"GameServices: UnlockAchievement {achievementId} success: {success}");
        });
        
        GameAnalytics.NewDesignEvent($"achievement:unlocked:{achievementId}");
    }

    private static void OnUserAuthenticated(bool success) {
        isAuthenticated = true;
    }
}
