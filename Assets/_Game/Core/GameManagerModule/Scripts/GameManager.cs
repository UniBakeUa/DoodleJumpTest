using System;
using System.Collections.Generic;
using _Game.Core.GameManagerModule.Scripts.States;
using _Game.Core.StateMashineModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.GameManagerModule.Scripts
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ModuleLoader _moduleLoader;
        public StateMachineBehaviour<StateBase> GameStateMachine { get; private set; }

        public GameManager(SceneLoader sceneLoader, ModuleLoader moduleLoader)
        {
            _sceneLoader = sceneLoader;
            _moduleLoader = moduleLoader;
        }
        public void Initialize()
        {
            GameStateMachine = new StateMachineBehaviour<StateBase>();

            GameStateMachine.SetStates(new List<StateBase>
            {
                new LoadingState(),
                new MenuState(),
                new PausedState(),
                new PlayingState(),
                new GameoverState(),
                new VictoryState()
            });

            _moduleLoader.OnLoadComplete += OnModulesLoaded;
        }

        public bool IsCurrentState<T>() where T : StateBase
        {
            return GameStateMachine.GetCurrentState() == typeof(T);
        }

        private void OnModulesLoaded()
        {
            GoToMenu();
        }

        public void GoToMenu()
        {
            GameStateMachine.Enter<MenuState>();
            _sceneLoader.LoadScene(1); 
        }

        public void StartNewGame()
        {
            GameStateMachine.Enter<PlayingState>();
            _sceneLoader.LoadScene(2);
        }

        public void PauseGame()
        {
            if (IsCurrentState<PlayingState>())
            {
                GameStateMachine.Enter<PausedState>();
            }
        }

        public void ResumeGame()
        {
            GameStateMachine.Enter<PlayingState>();
        }

        public void LoseGame() => GameStateMachine.Enter<GameoverState>();
        
        public void WinGame() => GameStateMachine.Enter<VictoryState>();
        public void Dispose()
        {
            GameStateMachine.Dispose();
        }
    }
}