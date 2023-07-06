#region

using System;
using System.Collections;
using _Scripts.Helpers;
using _Scripts.Systems;
using Unity.Mathematics;
using UnityEngine;

#endregion

namespace _Scripts
{
    public class Player : StaticInstance<Player>
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpDuration = 5f;
        [SerializeField] private float jumpHeight = 5f;

        [SerializeField] private float jumpRadius = 5f;

        private bool _isJumping;
        private Vector3 _jumpEndLocation;
        private Vector3 _jumpStartLocation;

        private float _previousJumpRadius;
        private Transform _transform;

        public float JumpRadius => jumpRadius;
        public float JumpDuration => jumpDuration;

        protected override void Awake()
        {
            base.Awake();

            _transform = transform;
            _previousJumpRadius = jumpRadius;
        }

        private void Start()
        {
            GameInput.Instance.OnJump += GameInputOnJump;
        }

        private void Update()
        {
            CalculateMovement();

            if (Math.Abs(_previousJumpRadius - jumpRadius) > 0.00001f)
            {
                OnLineWidthChanged?.Invoke();
                _previousJumpRadius = jumpRadius;
            }
        }

        public event Action OnLineWidthChanged;
        public event Action OnJumpStarted;
        public event Action OnJumpFinished;

        private void GameInputOnJump()
        {
            if (!_isJumping)
            {
                Vector2 movementVector = GameInput.Instance.GetMovementVectorNormalized();
                Vector3 movementVector3 = new Vector3(movementVector.x, 0, movementVector.y);

                _isJumping = true;
                _jumpStartLocation = _transform.position;
                _jumpEndLocation = _transform.position + movementVector3 * jumpRadius;

                StartCoroutine(PlayerJump());

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

        private IEnumerator PlayerJump()
        {
            float t = 0;

            while (t < jumpDuration)
            {
                t += Time.deltaTime;

                float tNorm = t/jumpDuration;
                float curX = Mathf.Lerp(_jumpStartLocation.x, _jumpEndLocation.x, tNorm);
                float curZ = Mathf.Lerp(_jumpStartLocation.z, _jumpEndLocation.z,tNorm);
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