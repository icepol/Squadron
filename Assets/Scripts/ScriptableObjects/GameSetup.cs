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
    
    [Header("Floor Settings")]
    public int floorVisibleRowsCount = 30;
    
    [Header("Spawn Settings")]
    public int floorMinRowsCountToSpawnObstacles = 5;
    public int floorMinRowsCountToSpawnCollectibles = 5;
    
    [Header("Row Behaviour Settings")]
    public float rowDelayBeforeShaking = 2;
    public float rowDelayBeforeFalling = 1;

    [Header("Background Settings")]
    public Color[] cameraBackgroundColors;
    
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
}
