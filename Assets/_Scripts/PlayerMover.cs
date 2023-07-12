#region

using System;
using System.Collections;
using _Scripts.Helpers;
using _Scripts.Systems;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace _Scripts
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpDuration = 0.1f;
        [SerializeField] private float jumpHeight = 5f;

        private bool _isJumping;
        private Vector3 _jumpEndLocation;
        private Vector3 _jumpStartLocation;
        private Transform _transform;

        public float JumpDuration => jumpDuration;

        private float _jumpRadius;

        public bool IsJumping => _isJumping;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            GameInput.Instance.OnJump += GameInputOnJump;
            _jumpRadius = Player.Instance.JumpRadius;
            Player.Instance.OnJumpRadiusChanged += PlayerOnJumpRadiusChanged;
        }

        private void PlayerOnJumpRadiusChanged()
        {
            _jumpRadius = Player.Instance.JumpRadius;
        }

        private void Update()
        {
            CalculateMovement();
        }

        public event Action OnJumpStarted;
        public event Action OnJumpFinished;

        private void GameInputOnJump(GameInput.JumpEventArgs obj)
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _jumpStartLocation = _transform.position;
                _jumpEndLocation = _transform.position + obj.JumpDirection * _jumpRadius;

                StartCoroutine(PlayerJumpCoroutine());

                OnJumpStarted?.Invoke();
            }
        }

        private void CalculateMovement()
        {
            Vector2 movementVector = GameInput.Instance.GetMovementVectorNormalized();
            Vector3 movementVector3 = new Vector3(movementVector.x, 0, movementVector.y);

            if (!_isJumping)
            {
                Move(movementVector3);
            }
        }

        private IEnumerator PlayerJumpCoroutine()
        {
            float t = 0;

            while (t < jumpDuration)
            {
                t += Time.deltaTime;

                float tNorm = t / jumpDuration;
                float curX = Mathf.Lerp(_jumpStartLocation.x, _jumpEndLocation.x, tNorm);
                float curZ = Mathf.Lerp(_jumpStartLocation.z, _jumpEndLocation.z, tNorm);
                float curY = MathHelper.GetParabolaHeight(jumpDuration, jumpHeight, tNorm);
                _transform.position = new Vector3(curX, curY, curZ);

                yield return null;
            }

            _transform.position = _jumpEndLocation;
            _isJumping = false;

            OnJumpFinished?.Invoke();
        }

        private void Move(Vector3 movementVector)
        {
            _transform.position += Time.deltaTime * speed * movementVector;
        }
    }
}