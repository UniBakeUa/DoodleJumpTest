using _Game.Content.Features.DeathLogic.Scripts;
using _Game.Content.Features.SpawnerModule.Scripts.Level;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.Behaivors
{
    public class Platform : MonoBehaviour, IPoolable<IMemoryPool>, IDestroyable
    {
        [Inject] private LevelGenerator _levelGenerator;    
        private IMemoryPool _pool;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent<IJumpable>(out var jumpable))
            {
                if (collision.relativeVelocity.y < 0)
                {
                    jumpable.Jump();
                }
            }
        }
        public void Despawn()
        {
            _levelGenerator.SpawnPlatform();
            _pool?.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<Platform> { }
        
        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
        }
        
        public void OnDespawned()
        {
            _pool = null;
            gameObject.SetActive(false);
        }

        public void DestroyThis()
        {
            Despawn();
        }
    }
}