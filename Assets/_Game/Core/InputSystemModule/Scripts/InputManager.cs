using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Game.Core.InputSystemModule.Scripts
{
    public class InputManager : IInitializable, IDisposable
    {
        public event Action<Vector2> OnSimpleClick;
        public event Action<Vector2> OnDragStart;
        public event Action<Vector2> OnDragging;
        public event Action OnDragEnd;
        
        public event Action OnLayoutChange;
        public InputSystem_Actions GameInput { get; private set; }
        public Layout CurrentLayout { get; private set; }

        private Vector2 _startMousePosition;
        private bool _isPotentialDrag;
        private bool _isDragging;
        private const float DragThreshold = 10f;

        public enum Layout
        {
            Gameplay,
            UI
        }

        #region Inputs
        private void SubscribeEvents()
        {
            GameInput.Gameplay.Click.started += OnPressStarted;
            GameInput.Gameplay.Click.canceled += OnPressCanceled;
            GameInput.Gameplay.Point.performed += OnPointerMoved;
        }

        private void OnPressStarted(InputAction.CallbackContext ctx)
        {
            _startMousePosition = GetPointerPosition();
            _isPotentialDrag = true;
            _isDragging = false;
        }
        private void OnPointerMoved(InputAction.CallbackContext ctx)
        {
            if (!_isPotentialDrag) return;

            Vector2 currentPos = ctx.ReadValue<Vector2>();

            if (!_isDragging)
            {
                CheckForDragStart(currentPos);
            }
            else
            {
                OnDragging?.Invoke(currentPos);
            }
        }

        private void CheckForDragStart(Vector2 currentPos)
        {
            if (Vector2.Distance(_startMousePosition, currentPos) > DragThreshold)
            {
                _isDragging = true;
                OnDragStart?.Invoke(_startMousePosition);
            }
        }

        private void OnPressCanceled(InputAction.CallbackContext ctx)
        {
            if (_isDragging)
            {
                OnDragEnd?.Invoke();
            }
            else if (_isPotentialDrag)
            {
                OnSimpleClick?.Invoke(_startMousePosition);
            }

            ResetDragState();
        }

        private void ResetDragState()
        {
            _isPotentialDrag = false;
            _isDragging = false;
        }

        public Vector2 GetPointerPosition()
        {
            return GameInput.Gameplay.Point.ReadValue<Vector2>();
        }
        #endregion

        public void Initialize()
        {
            GameInput = new InputSystem_Actions();
            GameInput.Gameplay.Enable();
            
            SubscribeEvents();
        }

        public void SwitchToGameplay()
        {
            GameInput.UI.Disable();
            GameInput.Gameplay.Enable();

            CurrentLayout = Layout.Gameplay;
            OnLayoutChange?.Invoke();
        }

        public void SwitchToUI()
        {
            GameInput.Gameplay.Disable();
            GameInput.UI.Enable();

            CurrentLayout = Layout.UI;
            OnLayoutChange?.Invoke();
        }

        public void Dispose()
        {
            GameInput.Gameplay.Click.started -= OnPressStarted;
            GameInput.Gameplay.Click.canceled -= OnPressCanceled;
            GameInput.Gameplay.Point.performed -= OnPointerMoved;
            GameInput.Dispose();
        }
    }
}