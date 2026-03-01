using System;
using System.Collections.Generic;
using _Game.Core.GameManagerModule.Scripts;
using _Game.Core.GameManagerModule.Scripts.States;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.ScoreModule.Scripts
{
    public class ScoreManager : IInitializable, ITickable
    {
        public event Action<int> OnScoreChanged;

        private List<RunData> _pastRuns = new();
        public int CurrentScore { get; private set; }

        private Transform _trackingPoint;
        private float _startY;

        [Inject] private GameManager _gameManager;
        
        [Serializable]
        public struct RunData
        {
            public string Date;
            public int Score;
        }

        public void Initialize()
        {
            LoadHistory();
        }
        
        public void StartNewRun(Transform target)
        {
            _trackingPoint = target;
            _startY = target.position.y;
            CurrentScore = 0;
            
            OnScoreChanged?.Invoke(CurrentScore);
        }

        public void Tick()
        {
            if (_gameManager.IsCurrentState<PlayingState>() && _trackingPoint != null)
            {
                UpdateScore();
            }
        }

        private void UpdateScore()
        {
            int height = (int)(_trackingPoint.position.y - _startY);
            
            if (height > CurrentScore)
            {
                CurrentScore = height;
                OnScoreChanged?.Invoke(CurrentScore);
            }
        }

        public void FinalizeRun()
        {
            if (CurrentScore <= 0) return;

            RunData run = new RunData
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
                Score = CurrentScore
            };

            _pastRuns.Add(run);
            SaveHistory();

            _trackingPoint = null;
        }

        public List<RunData> GetPastRuns() => _pastRuns;

        private void SaveHistory()
        {
            //opportunity to save
        }

        private void LoadHistory()
        {
            //opportunity to load
        }
    }
}