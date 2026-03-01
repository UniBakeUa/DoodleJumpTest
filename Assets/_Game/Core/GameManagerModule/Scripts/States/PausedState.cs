using System;
using _Game.Core.StateMashineModule.Scripts;
using UnityEngine;

namespace _Game.Core.GameManagerModule.Scripts.States
{
    public class PausedState : StateBase
    {
        public override void Enter()
        {
            Debug.Log("Game is PAUSED");
            Time.timeScale = 0;
        }

        public override void Exit()
        {
            Debug.Log("Exiting PAUSED");
            Time.timeScale = 1;
        }
    }
}