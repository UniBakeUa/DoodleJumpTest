using UnityEngine;
using Zenject;

namespace _Game.Content.Features.ScoreModule.Scripts
{
    [CreateAssetMenu(menuName = "Game/ScoreInstaller", fileName = "ScoreInstaller")]
    public class ScoreInstaller : ScriptableObjectInstaller<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>()
                .AsSingle().NonLazy();
        }
    }
}