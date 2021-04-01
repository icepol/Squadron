using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "Assets/Level Setup")]
public class LevelSetup : ScriptableObject
{
    public int squadronsPerLevel;
    public float enemyRatio;
    public int enemiesInSquadron;

    public EnemySquadron[] enemySquadrons;
    public Enemy[] enemies;

    public EnemyCity[] enemyCities;
    public float enemyCityRatio;
}
