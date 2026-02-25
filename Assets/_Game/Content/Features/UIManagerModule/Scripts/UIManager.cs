using _Game.Core;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.UIManagerModule.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas recordsCanvas;
        [SerializeField] private Canvas privacyPolicyCanvas;
        
        [Inject] private SceneLoader _sceneLoader;

        public void OpenMenu()
        {
            recordsCanvas.gameObject.SetActive(false);
            privacyPolicyCanvas.gameObject.SetActive(false);
        }
        public void StartGame() //button event
        {
            _sceneLoader.LoadScene(2);
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