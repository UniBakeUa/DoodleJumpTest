using System;
using _Game.Core;
using Zenject;

namespace _Game.Content.Features.Scene
{
    public class BootstrapSceneChanger : IInitializable,IDisposable
    {
        [Inject] private ModuleLoader _moduleLoader;
        [Inject] private SceneLoader _sceneLoader;
        public void Initialize()
        {
            if (_moduleLoader.loaderType == ModuleLoader.LoaderType.Bootstrap)
                _moduleLoader.OnLoadComplete += OnBootstrapComplete;
        }

        public void Dispose()
        {
            _moduleLoader.OnLoadComplete -= OnBootstrapComplete;
        }

        private void OnBootstrapComplete() =>
            _sceneLoader.LoadScene((int)ModuleLoader.LoaderType.MainMenu);
    }
}