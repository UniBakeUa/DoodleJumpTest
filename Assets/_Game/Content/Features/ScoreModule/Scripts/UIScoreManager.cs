using TMPro;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.ScoreModule.Scripts
{
    public class UIScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text uiScoreManager;
        
        private ScoreManager _scoreManager;
        
        [Inject]
        public void Construct(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
            
            _scoreManager.OnScoreChanged += ChangeScoreText;
        }
        private void ChangeScoreText(int score)
        {
            uiScoreManager.text = $"Score: {score.ToString()}";
        }

        private void OnDestroy()
        {
            _scoreManager.OnScoreChanged -= ChangeScoreText;
        }
    }
}