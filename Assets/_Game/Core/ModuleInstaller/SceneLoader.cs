using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Core
{
    public class SceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
            
            Debug.Log($"[SceneLoader] {sceneId} loaded!");
        }
    }
}