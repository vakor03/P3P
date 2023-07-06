#region

using System;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace _Scripts.Systems
{
    public sealed class GameInput : StaticInstance<GameInput>
    {
        private PlayerInputActions _playerInputActions;

        protected override void Awake()
        {
            base.Awake();

            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Jump.performed += JumpOnPerformed;
        }

        private void JumpOnPerformed(InputAction.CallbackContext obj)
        {
            OnJump?.Invoke();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            return inputVector.normalized;
        }

        public event Action OnJump;
    }
}