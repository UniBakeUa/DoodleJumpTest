using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Core
{
    public class SceneLoader
    {
        public async Task LoadScene(int sceneId)
        {
            var operation = SceneManager.LoadSceneAsync(sceneId);
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            
            Debug.Log($"[SceneLoader] {sceneId} loaded");
        }
    }
}