using UnityEngine;

namespace _Game.Content.Features.DeathLogic.Scripts
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDestroyable>(out var destroyable))
            {
                destroyable.DestroyThis();
            }
        }
    }
}