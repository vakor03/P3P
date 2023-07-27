using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class ScoreCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private void Awake()
        {
            SetDefaultText();
        }

        private void Start()
        {
            ScoreCounter.Instance.OnScoreChanged += ScoreCounterOnScoreChanged;
        }

        private void ScoreCounterOnScoreChanged()
        {
            ChangeScoreText(ScoreCounter.Instance.Score);
        }

        private void ChangeScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        private void SetDefaultText()
        {
            ChangeScoreText(0);
        }
    }
}