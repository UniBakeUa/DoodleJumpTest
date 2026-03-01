using _Game.Content.Features.ScoreModule.Scripts;
using _Game.Core.StateMashineModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.GameManagerModule.Scripts.States
{
    public class GameoverState : StateBase
    {
        [Inject] ScoreManager _scoreManager;
        public override void Enter()
        {
            Debug.Log("Game Over");
            _scoreManager.FinalizeRun();
        }

        public override void Exit() => Debug.Log("Exiting GameOver");
    }
}