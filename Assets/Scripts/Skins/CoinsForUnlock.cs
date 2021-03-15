using UnityEngine;

public class CoinsForUnlock : MonoBehaviour
{
    [SerializeField] private TextMesh[] text;

    public void SetRequiredValue(int value)
    {
        foreach (TextMesh text in text)
        {
            text.text = $"x{value}";
        }
    }
}
