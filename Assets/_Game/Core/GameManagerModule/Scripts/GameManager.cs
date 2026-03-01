using System;
using System.Collections.Generic;
using _Game.Core.GameManagerModule.Scripts.States;
using _Game.Core.StateMashineModule.Scripts;
using Zenject;

namespace _Game.Core.GameManagerModule.Scripts
{
    public class GameManager : IInitializable, IDisposable
    {
        public event Action GameOver;
        
        private readonly SceneLoader _sceneLoader;
        private readonly ModuleLoader _moduleLoader;
        private readonly IInstantiator _instantiator;
        public StateMachineBehaviour<StateBase> GameStateMachine { get; private set; }

        public GameManager(SceneLoader sceneLoader, ModuleLoader moduleLoader, IInstantiator instantiator)
        {
            _sceneLoader = sceneLoader;
            _moduleLoader = moduleLoader;
            _instantiator = instantiator;
        }
        public void Initialize()
        {
            GameStateMachine = new StateMachineBehaviour<StateBase>();

            GameStateMachine.SetStates(new List<StateBase>
            {
                _instantiator.Instantiate<LoadingState>(),
            });

            GameStateMachine.Enter<LoadingState>();
            
            _moduleLoader.OnLoadComplete += OnModulesLoaded;
        }

        public bool IsCurrentState<T>() where T : StateBase
        {
            return GameStateMachine.GetCurrentState() == typeof(T);
        }

        private void OnModulesLoaded()
        {
            GameStateMachine.SetStates(new List<StateBase>
            {
                _instantiator.Instantiate<LoadingState>(),
                _instantiator.Instantiate<MenuState>(),
                _instantiator.Instantiate<PausedState>(),
                _instantiator.Instantiate<PlayingNewState>(),
                _instantiator.Instantiate<PlayingState>(),
                _instantiator.Instantiate<GameoverState>(),
                _instantiator.Instantiate<VictoryState>()
            });
            
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            
            if (currentSceneIndex == 0)
            {
                GoToMenu();
            }
            else if (currentSceneIndex == 1)
            {
                GameStateMachine.Enter<MenuState>();
            }
            else if (currentSceneIndex == 2)
            {
                GameStateMachine.Enter<PlayingNewState>();
            }
        }

        public async void GoToMenu()
        {
            await _sceneLoader.LoadScene(1);
            GameStateMachine.Enter<MenuState>();
        }

        public async void StartGame()
        {
            await _sceneLoader.LoadScene(2);
            GameStateMachine.Enter<PlayingNewState>();
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

        public void LoseGame()
        {
            GameStateMachine.Enter<GameoverState>();
            GameOver?.Invoke();
        }

        public void WinGame() => GameStateMachine.Enter<VictoryState>();
        public void Dispose()
        {
            GameStateMachine.Dispose();
        }
    }
}