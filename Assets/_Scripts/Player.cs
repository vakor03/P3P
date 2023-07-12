#region

using System;
using _Scripts.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Scripts
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : StaticInstance<Player>, IDamageable
    {
        [SerializeField] private float jumpRadiusModifier = 1f;
        [SerializeField] private float radiusOffset = 0.5f;

        private readonly int _stepsCount = 6;

        private int _jumpNumber;
        private int _health = 3;

        private bool _invincible;

        private float _jumpRadius;

        private PlayerAttacker _playerAttacker;
        private PlayerMover _playerMover;
        private Transform _transform;
        public float JumpRadius => _jumpRadius;
        public int JumpNumber => _jumpNumber;
        public float JumpDuration => _playerMover.JumpDuration;

        protected override void Awake()
        {
            base.Awake();

            _playerMover = GetComponent<PlayerMover>();
            _playerAttacker = GetComponent<PlayerAttacker>();
            
            ChangeJumpRadius();
        }

        private void Start()
        {
            _playerMover.OnJumpStarted += PlayerMoverOnJumpStarted;
            _playerMover.OnJumpFinished += PlayerMoverOnJumpFinished;
        }

        public event Action OnJumpRadiusChanged;
        public event Action OnDamageTaken;

        private void PlayerMoverOnJumpStarted()
        {
            _invincible = true;
        }

        private void PlayerMoverOnJumpFinished()
        {
            _invincible = false;

            _playerAttacker.PerformAttack();
            ChangeJumpRadius();
            OnJumpRadiusChanged?.Invoke();
        }

        private void ChangeJumpRadius()
        {
            int oldStepsCount = _jumpNumber;
            while (oldStepsCount == _jumpNumber)
            {
                _jumpNumber = Random.Range(1, 1 + _stepsCount);
            }

            _jumpRadius = _jumpNumber * jumpRadiusModifier + radiusOffset;
        }

        public void TakeDamage()
        {
            if (_invincible)
            {
                return;
            }

            _health--;
            OnDamageTaken?.Invoke();
        }
    }
}