using UnityEngine;
using Zenject;

namespace _Game.Content.Features.ScoreModule.Scripts.UIMenu
{
    public class UIScoreHistoryProvider : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private UIScoreRow rowPrefab;

        [Inject] private ScoreManager _scoreManager;

        public void DisplayHistory()
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            var history = _scoreManager.GetPastRuns();

            for (int i = history.Count - 1; i >= 0; i--)
            {
                var run = history[i];
                UIScoreRow rowInstance = Instantiate(rowPrefab, container);
                rowInstance.Setup(run.Date, run.Score);
            }
        }

        private void OnEnable()
        {
            DisplayHistory();
        }
    }
}