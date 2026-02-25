using _Game.Content.Features.Scene;
using Zenject;

namespace _Game.Core
{
    public class LoaderInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<BootstrapSceneChanger>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<ModuleLoader>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}