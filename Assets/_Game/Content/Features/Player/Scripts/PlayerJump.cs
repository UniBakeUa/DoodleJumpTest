using UnityEngine;

namespace _Game.Content.Features.Behaivors
{
    public class PlayerJump : MonoBehaviour, IJumpable
    {
        [SerializeField] private float jumpForce;
        
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            SmallJump();
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        private void SmallJump()
        {
            _rigidbody.AddForce(Vector2.up * jumpForce/10, ForceMode2D.Impulse);
        }
    }
}