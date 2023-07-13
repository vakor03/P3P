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
            // LevelBoundaries.Instance.OnCollide += LevelBoundariesOnCollide;
        }

        private void LevelBoundariesOnCollide()
        {
            throw new NotImplementedException();
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
            _canContinueJump = true;

            while (t < jumpDuration)
            {
                t += Time.deltaTime;
                if (_canContinueJump)
                {
                    var jumpSpeed = (_jumpEndLocation - _jumpStartLocation).magnitude / jumpDuration;

                    bool canMove = !Physics.BoxCast(boxCollider.center + transform.position,
                        boxCollider.size / 2, (_jumpEndLocation - _jumpStartLocation), Quaternion.identity
                        , jumpSpeed * Time.deltaTime, obstaclesLayerMask);
                    if (!canMove)
                    {
                        _jumpEndLocation = _transform.position;
                        _jumpEndLocation.y = _jumpStartLocation.y;
                        _canContinueJump = false;
                    }
                }

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

        private bool _canContinueJump;

        private bool _isMoving;

        private void Move(Vector3 movementVector)
        {
            // _transform.position += Time.deltaTime * speed * movementVector;

            _isMoving = movementVector != Vector3.zero;

            var canMove = TryMoveInDirection(speed * Time.deltaTime, ref movementVector);

            if (canMove)
            {
                transform.position += movementVector * (speed * Time.deltaTime);
            }
        }

        [SerializeField] private LayerMask obstaclesLayerMask;
        [SerializeField] private BoxCollider boxCollider;


        private bool TryMoveInDirection(float moveDistance, ref Vector3 moveDirection)
        {
            bool canMove = !Physics.BoxCast(boxCollider.center + transform.position,
                boxCollider.size / 2, moveDirection, Quaternion.identity
                , moveDistance, obstaclesLayerMask);

            if (!canMove)
            {
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                if ((moveDirection.x < -.5f || moveDirection.x > +.5f) && !Physics.BoxCast(
                        boxCollider.center + transform.position,
                        boxCollider.size / 2, moveDirectionX, transform.rotation
                        , moveDistance, obstaclesLayerMask))
                {
                    moveDirection = moveDirectionX;
                    return true;
                }

                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;

                if ((moveDirection.x < -.5f || moveDirection.x > +.5f) && !Physics.BoxCast(
                        boxCollider.center + transform.position,
                        boxCollider.size / 2, moveDirectionZ, transform.rotation
                        , moveDistance, obstaclesLayerMask))
                {
                    moveDirection = moveDirectionZ;
                    return true;
                }
            }

            return canMove;
        }
    }
}