using UnityEngine;
using UnityEngine.UI;

namespace pixelook
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private Text score;

        void Update()
        {
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            score.text = GameState.Score.ToString();
        }
    }
}