using _Game.Content.Features.JumpLogic.Scripts;
using _Game.Content.Features.ScoreModule.Scripts;
using _Game.Content.Features.SpawnerModule.Scripts;
using _Game.Core.StateMashineModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.GameManagerModule.Scripts.States
{
    public class PlayingNewState: StateBase
    {
        [Inject] private ScoreManager _scoreManager;
        [Inject] private PlayerSpawner _playerSpawner;
        [Inject] private TrackingPointSpawner _trackingPointSpawner;
        [Inject] private GameManager _gameManager;
        public override void Enter()
        {
            Debug.Log("Starting new game");
            TrackingPoint point = _trackingPointSpawner.SpawnTracking();
            _playerSpawner.SpawnPlayer();
            _scoreManager.StartNewRun(point.transform);
            
            _gameManager.ResumeGame();
        }

        public override void Exit()
        {
            
        }
    }
}