using UnityEngine;

[CreateAssetMenu(fileName = "RatingSetup", menuName = "Assets/Rating Setup")]
public class RatingSetup : LoadSaveScriptableObject
{
    private const string FILENAME = "rating_setup.json";
    
    [Header("Current state")]
    [SerializeField] private int attemptsCount;
    [SerializeField] private int finishedGames;
    [SerializeField] private bool isRated;
    
    public int AttemptsCount
    {
        get => attemptsCount;
        set {
            attemptsCount = value;
            
            SaveToFile(FILENAME);
        }
    }
    
    public int FinishedGames
    {
        get => finishedGames;
        set {
            finishedGames = value;
            
            SaveToFile(FILENAME);
        }
    }
    
    public bool IsRated
    {
        get => isRated;
        set {
            isRated = value;
            
            SaveToFile(FILENAME);
        }
    }

    [Header("Rating setup")]
    public int finishedGamesToShow = 5;
    public int maxAttemptsCount = 3;
    
    [Header("Build setup")]
    public bool isProduction;

    public void LoadFromFile()
    {
        LoadFromFile(FILENAME);
    }

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;
        
        AttemptsCount = 0;
        FinishedGames = 0;
        IsRated = false;
    }
}
