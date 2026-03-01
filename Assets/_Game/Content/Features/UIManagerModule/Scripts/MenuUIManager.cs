using _Game.Core;
using _Game.Core.GameManagerModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.UIManagerModule.Scripts
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas recordsCanvas;
        [SerializeField] private Canvas privacyPolicyCanvas;
        
        [Inject] private GameManager _gameManager;

        public void OpenMenu()
        {
            recordsCanvas.gameObject.SetActive(false);
            privacyPolicyCanvas.gameObject.SetActive(false);
        }
        public void StartGame() //button event
        {
            _gameManager.StartGame();
        }
        public void OpenRecords() //button event
        {
            privacyPolicyCanvas.gameObject.SetActive(false);
            
            recordsCanvas.gameObject.SetActive(true);
        }
        public void OpenPrivacyPolicy() //button event
        {
            recordsCanvas.gameObject.SetActive(false);
            
            privacyPolicyCanvas.gameObject.SetActive(true);
        }
    }
}