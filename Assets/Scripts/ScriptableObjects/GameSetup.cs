using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetup", menuName = "Assets/Game Setup")]
public class GameSetup : LoadSaveScriptableObject, IResetBeforeBuild
{
    private const string FILENAME = "game_setup.json";
    
    [Header("Skin Settings")]
    public int selectedSkinIndex;
    public bool areUnlockedAll;

    public int SelectedSkinIndex
    {
        get => selectedSkinIndex;
        set
        {
            selectedSkinIndex = value;
            
            SaveToFile(FILENAME);
        }
    }

    public bool AreUnlockedAll
    {
        get => areUnlockedAll;
        set
        {
            areUnlockedAll = value;
            
            SaveToFile(FILENAME);
        }
    }

    public SkinSetup[] skins;
    
    [Header("Spawn Settings")]
    public int minSpawnedSquadrons;
    public int minSquadronCountToSpawnEnemies = 5;
    public int minSquadronCountToSpawnCities = 10;

    [Header("Levels Settings")]
    public LevelSetup[] levels;
    
    [Header("Build setup")]
    public bool isProduction;

    public void LoadFromFile()
    {
        LoadFromFile(FILENAME);

        foreach (SkinSetup skinSetup in skins)
        {
            skinSetup.LoadFromFile();
        }
    }

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;
        
        SelectedSkinIndex = 0;
        AreUnlockedAll = false;
    }
    
    public int LevelNumberBySpawnedSquadrons
    {
        get
        {
            int rows = 0;
            int levelNumber = 0;

            foreach (LevelSetup levelSetup in levels)
            {
                levelNumber++;
                rows += levelSetup.squadronsPerLevel;

                if (GameState.SpawnedSquadronsCount <= rows)
                    // this is the level we are currently spawning for
                    break;
            }

            return levelNumber < levels.Length ? levelNumber : levels.Length;
        }
    }

    public LevelSetup CurrentLevel => levels[LevelNumberBySpawnedSquadrons - 1];
}
