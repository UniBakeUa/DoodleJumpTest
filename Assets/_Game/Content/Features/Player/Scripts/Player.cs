using _Game.Content.Features.DeathLogic.Scripts;
using _Game.Core.GameManagerModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.Behaivors
{
    public class Player : MonoBehaviour, IDestroyable
    {
        [Inject] private GameManager _gameManager;
        public void DestroyThis()
        {
            _gameManager.LoseGame();
        }
    }
}