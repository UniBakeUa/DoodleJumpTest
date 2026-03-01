using _Game.Content.Features.Behaivors;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.SpawnerModule.Scripts.Level
{
    [CreateAssetMenu(menuName = "Game/LevelSpawnerInstaller", fileName = "LevelSpawnerInstaller")]
    public class LevelSpawnerInstaller : ScriptableObjectInstaller<LevelSpawnerInstaller>
    {
        [SerializeField] private LevelGeneratorConfig levelConfig;
        [SerializeField] private GameObject[] platformPrefabs;

        public override void InstallBindings()
        {
            Container.BindInstance(levelConfig).AsSingle();

            Container.BindFactory<Platform, Platform.Factory>()
                .FromPoolableMemoryPool<Platform, PlatformPool>(poolBinder => poolBinder
                    .WithInitialSize(levelConfig.initialSpawn)
                    .FromMethod(CreatePlatform));
            
            Container.BindInterfacesAndSelfTo<LevelGenerator>()
                .AsSingle()
                .NonLazy();
        }
        private Platform CreatePlatform(DiContainer container)
        {
            var prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            // Використовуємо InstantiatePrefabForComponent, щоб Zenject прокинув залежності
            return container.InstantiatePrefabForComponent<Platform>(prefab);
        }
        class PlatformPool : MonoPoolableMemoryPool<IMemoryPool, Platform> { }
    }
}