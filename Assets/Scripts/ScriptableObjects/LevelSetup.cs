using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "Assets/Level Setup")]
public class LevelSetup : ScriptableObject
{
    public int numberOfRows;
    public float collectibleRatio;
    public float obstacleRatio;
}
