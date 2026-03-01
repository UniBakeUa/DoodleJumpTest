using System;
using _Game.Core.GameManagerModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.UIManagerModule.Scripts
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuWindow;
        [SerializeField] private GameObject gameOverWindow;

        [Inject] private GameManager _gameManager;

        private void Awake()
        {
            _gameManager.GameOver += GameOver;
        }

        public void PauseGame() //button event
        {
            _gameManager.PauseGame();
            menuWindow.SetActive(true);
        }

        public void ResumeGame() //button event
        {
            _gameManager.ResumeGame();
            menuWindow.SetActive(false);
        }

        public void MenuGame() //button event
        {
            _gameManager.GoToMenu();
        }

        public void RestartGame() //button event
        {
            _gameManager.StartGame();
        }

        public void GameOver()
        {
            gameOverWindow.SetActive(true);
        }

        private void OnDestroy()
        {
            _gameManager.GameOver -= GameOver;
        }
    }
}