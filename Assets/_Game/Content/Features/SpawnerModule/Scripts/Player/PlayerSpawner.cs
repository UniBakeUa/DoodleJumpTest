using System;
using _Game.Content.Features.Behaivors;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.SpawnerModule.Scripts
{
    public class PlayerSpawner
    {
        public event Action<Player> OnPlayerSpawned;
        
        private Player _playerPrefab;
        private readonly IInstantiator _instantiator;

        public PlayerSpawner(Player playerPrefab, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            
            SetPlayerPrefab(playerPrefab);
        }
        public void SetPlayerPrefab(Player playerPrefab)
        {
            _playerPrefab = playerPrefab;
        }

        public void SpawnPlayer()
        {
            var sceneContext = GameObject.FindObjectOfType<SceneContext>();
            Transform parentTransform = sceneContext != null ? sceneContext.transform : null;
            
            var playerInstance = _instantiator.InstantiatePrefabForComponent<Player>(
                _playerPrefab, 
                Vector3.zero, 
                Quaternion.identity, 
                parentTransform);
            Debug.Log("Spawned player: " + playerInstance.gameObject.name);
            
            OnPlayerSpawned?.Invoke(playerInstance);
        }
    }
}