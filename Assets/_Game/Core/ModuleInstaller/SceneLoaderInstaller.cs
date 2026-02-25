using UnityEngine;
using Zenject;

namespace _Game.Core
{
    [CreateAssetMenu(menuName = "Game/SceneLoaderInstaller", fileName = "SceneLoaderInstaller")]
    public class SceneLoaderInstaller : ScriptableObjectInstaller<SceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}