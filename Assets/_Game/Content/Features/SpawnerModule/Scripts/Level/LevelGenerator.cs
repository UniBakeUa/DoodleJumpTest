using UnityEngine;
using _Game.Content.Features.Behaivors;
using Zenject;

namespace _Game.Content.Features.SpawnerModule.Scripts.Level
{
    public class LevelGenerator : IInitializable
    {
        private readonly Platform.Factory _factory;
        private readonly LevelGeneratorConfig _config;
        private readonly Camera _mainCamera;

        private float _lastSpawnY;

        public LevelGenerator(Platform.Factory factory, LevelGeneratorConfig config)
        {
            _factory = factory;
            _config = config;
            _mainCamera = Camera.main;
            
            _lastSpawnY = _config.startY - _config.maxStepY; 
        }

        public void SpawnPlatform()
        {
            float screenHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
            float maxRangeX = screenHalfWidth - _config.sidePadding;
            float randomX = Random.Range(-maxRangeX, maxRangeX);
            
            float stepY = Random.Range(_config.minStepY, _config.maxStepY);
            float nextY = _lastSpawnY + stepY;

            Vector2 position = new Vector2(randomX, nextY);
            
            SpawnPlatformInPoint(position);
            
            _lastSpawnY = nextY;
        }

        public void SpawnPlatformInPoint(Vector2 position)
        {
            var platform = _factory.Create();
            platform.transform.position = position;
        }

        public void Initialize()
        {
            for (int i = 0; i < _config.initialSpawn; i++)
            {
                SpawnPlatform();
            }
        }
    }
}