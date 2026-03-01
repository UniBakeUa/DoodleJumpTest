using _Game.Content.Features.Behaivors;
using _Game.Content.Features.SpawnerModule.Scripts;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.JumpLogic.Scripts
{
    public class TrackingPoint : MonoBehaviour
    {
        private float _previousPosition;
        private Player _player;
        private CinemachineCamera _camera;

        [Inject] private PlayerSpawner _playerSpawner;

        private void Awake()
        {
            _camera = FindObjectOfType<CinemachineCamera>();
            _playerSpawner.OnPlayerSpawned += OnNewPlayer;
        }

        private void OnNewPlayer(Player player)
        {
            _player = player;
            _camera.Follow = gameObject.transform;
        }
        private void Update()
        {
            if(!_player)
                return;
            
            if (_player.transform.position.y > _previousPosition)
            {
                SetHighestPoint(_player.transform.position.y);
            }
        }

        private void SetHighestPoint(float y)
        {
            _previousPosition = y;
            
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        private void OnDestroy()
        {
            _playerSpawner.OnPlayerSpawned -= OnNewPlayer;
        }
    }
}