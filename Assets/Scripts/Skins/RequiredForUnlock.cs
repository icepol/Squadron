using UnityEngine;

public class RequiredForUnlock : MonoBehaviour
{
    [SerializeField] private TextMesh[] text;

    public void SetRequiredValue(int value)
    {
        foreach (TextMesh text in text)
        {
            text.text = $"{value}";
        }
    }
}
