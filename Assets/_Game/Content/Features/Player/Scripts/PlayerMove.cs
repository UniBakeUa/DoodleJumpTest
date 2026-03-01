using _Game.Core.InputSystemModule.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Content.Features.Behaivors
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float smoothTime;

        private InputManager _inputManager;
        private Camera _mainCamera;
        private float _targetX;
        private float _currentVelocity;

        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            _targetX = transform.position.x;
            
            _inputManager.OnSimpleClick += UpdateTargetPosition;
            _inputManager.OnDragging += UpdateTargetPosition;

            _inputManager.OnDragEnd += TargetPositionZero;
        }

        private void FixedUpdate()
        {
            float newX = Mathf.SmoothDamp(transform.position.x, _targetX, ref _currentVelocity, smoothTime, speed);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void UpdateTargetPosition(Vector2 screenPosition)
        {
            Vector3 worldPoint =
                _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y,
                    _mainCamera.nearClipPlane));
            _targetX = worldPoint.x;
        }
        private void TargetPositionZero()
        {
            _targetX = transform.position.x;
        }

        private void OnDestroy()
        {
            if (_inputManager != null)
            {
                _inputManager.OnSimpleClick -= UpdateTargetPosition;
                _inputManager.OnDragging -= UpdateTargetPosition;
                _inputManager.OnDragEnd -= TargetPositionZero;
            }
        }
    }
}