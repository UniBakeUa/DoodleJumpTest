using _Game.Content.Features.SpawnerModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.JumpLogic.Scripts
{
    [CreateAssetMenu(menuName = "Game/TrackingPointInstaller", fileName = "TrackingPointInstaller")]
    public class TrackingPointInstaller : ScriptableObjectInstaller<TrackingPointInstaller>
    {
            [SerializeField] private TrackingPoint trackingPrefab;
            public override void InstallBindings()
            {
                Container.BindInterfacesAndSelfTo<TrackingPointSpawner>()
                    .AsSingle()
                    .WithArguments(trackingPrefab)
                    .NonLazy();
        }
    }
}