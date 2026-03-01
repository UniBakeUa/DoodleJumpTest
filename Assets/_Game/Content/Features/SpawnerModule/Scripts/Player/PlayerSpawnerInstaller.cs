using _Game.Content.Features.Behaivors;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.SpawnerModule.Scripts
{
    [CreateAssetMenu(menuName = "Game/PlayerSpawnerInstaller", fileName = "PlayerSpawnerInstaller")]
    public class PlayerSpawnerInstaller : ScriptableObjectInstaller<PlayerSpawnerInstaller>
    {
        [SerializeField] private Player playerPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerSpawner>()
                .AsSingle()
                .WithArguments(playerPrefab)
                .NonLazy();
        }
    }
}