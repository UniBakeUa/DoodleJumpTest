using _Game.Core.GameManagerModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core
{
    public class LoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoaderSettings loaderSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(loaderSettings).AsSingle();
            
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ModuleLoader>().AsSingle().NonLazy();
        }
    }

}