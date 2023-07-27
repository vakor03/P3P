#region

using System;
using _Scripts.Helpers;
using _Scripts.Units.Players;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace _Scripts.Systems
{
    public sealed class GameInput : StaticInstance<GameInput>
    {
        private Vector2 _lastMovementVector;
        private PlayerInputActions _playerInputActions;

        protected override void Awake()
        {
            base.Awake();

            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Jump.performed += JumpOnPerformed;
        }

        private void Start()
        {
            if (Ground.Instance != null)
            {
                Ground.Instance.OnClicked += GroundOnClicked;
            }
        }

        private void GroundOnClicked(Ground.GroundClickedEventArgs obj)
        {
            if (Helper.IsOverUI())
            {
                return;
            }
            Vector3 jumpDirection = obj.HitPoint - Player.Instance.transform.position;
            jumpDirection.y = 0;
            OnJump?.Invoke(new JumpEventArgs { JumpDirection = jumpDirection.normalized });
        }

        private void JumpOnPerformed(InputAction.CallbackContext obj)
         {
            Vector3 jumpDirection = new Vector3(_lastMovementVector.x, 0, _lastMovementVector.y);
            OnJump?.Invoke(new JumpEventArgs(){JumpDirection = jumpDirection});
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();

            _lastMovementVector = inputVector;
            return inputVector;
        }

        public event Action<JumpEventArgs> OnJump;

        public struct JumpEventArgs
        {
            public Vector3 JumpDirection;
        }
    }
}