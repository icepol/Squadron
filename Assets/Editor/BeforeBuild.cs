using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BeforeBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        foreach (GameSetup item in Resources.FindObjectsOfTypeAll<GameSetup>())
            item.ResetBeforeBuild();

        foreach (RatingSetup item in Resources.FindObjectsOfTypeAll<RatingSetup>())
            item.ResetBeforeBuild();

        foreach (SkinSetup item in Resources.FindObjectsOfTypeAll<SkinSetup>())
            item.ResetBeforeBuild();
    }
}
