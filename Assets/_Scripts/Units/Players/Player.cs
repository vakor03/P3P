#region

using System;
using _Scripts.Animations;
using _Scripts.HealthSystems;
using _Scripts.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Scripts.Units.Players
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : StaticInstance<Player>, IPlayer
    {
        [SerializeField] private float jumpRadiusModifier = 1f;
        [SerializeField] private float radiusOffset = 0.5f;
        [SerializeField] private Transform playerVisuals;


        private bool _invincible;


        private PlayerAttacker _playerAttacker;
        private PlayerMover _playerMover;
        private DamageReceiver _damageReceiver;
        private JumpRadiusController _jumpRadiusController;
        public JumpRadiusController JumpRadiusController => _jumpRadiusController;

            private IUnitHealth _unitHealth;

        public IUnitHealth UnitHealth => _unitHealth;
        public IAnimation Animation => _animation;

        private IAnimation _animation;

        private Transform _transform;

        public float JumpDuration => _playerMover.JumpDuration;

        protected override void Awake()
        {
            base.Awake();

            _playerMover = GetComponent<PlayerMover>();
            _playerAttacker = GetComponent<PlayerAttacker>();
            _damageReceiver = GetComponent<DamageReceiver>();
            _unitHealth = new UnitHealth(100);
            _animation = new RotateAnimation(playerVisuals, JumpDuration);
            _playerMover.OnJumpStarted += _animation.PerformAnimation;
            _jumpRadiusController = new JumpRadiusController(jumpRadiusModifier, radiusOffset);

            _jumpRadiusController.SetDefaultJumpRadius();
        }

        private void Start()
        {
            _playerMover.OnJumpStarted += PlayerMoverOnJumpStarted;
            _playerMover.OnJumpFinished += PlayerMoverOnJumpFinished;
            _damageReceiver.OnDamageReceived += DamageReceiverOnDamageReceived;
        }

        private void DamageReceiverOnDamageReceived(int value)
        {
            _unitHealth.ReceiveDamage(value);
        }


        private void PlayerMoverOnJumpStarted()
        {
            _damageReceiver.IsInvincible = true;
        }

        private void PlayerMoverOnJumpFinished()
        {
            _damageReceiver.IsInvincible = false;

            _playerAttacker.PerformAttack();
            _jumpRadiusController.ChangeJumpRadius();
        }
    }

    public class JumpRadiusController : IJumpRadiusController
    {
        private readonly float _jumpRadiusModifier;
        private readonly float _radiusOffset;

        public JumpRadiusController(float jumpRadiusModifier, float radiusOffset)
        {
            _jumpRadiusModifier = jumpRadiusModifier;
            _radiusOffset = radiusOffset;
        }

        private readonly int _stepsCount = 6;

        public float JumpRadius { get; private set; }
        public int JumpNumber { get; private set; }
        public event Action OnJumpRadiusChanged;

        public void ChangeJumpRadius()
        {
            int oldStepsCount = JumpNumber;
            while (oldStepsCount == JumpNumber)
            {
                JumpNumber = Random.Range(1, 1 + _stepsCount);
            }

            JumpRadius = JumpNumber * _jumpRadiusModifier + _radiusOffset;

            OnJumpRadiusChanged?.Invoke();
        }

        public void SetDefaultJumpRadius()
        {
            int oldStepsCount = JumpNumber;
            while (oldStepsCount == JumpNumber)
            {
                JumpNumber = Random.Range(1, 1 + _stepsCount);
            }

            JumpRadius = JumpNumber * _jumpRadiusModifier + _radiusOffset;
        }
    }

    public interface IJumpRadiusController
    {
        float JumpRadius { get; }
        int JumpNumber { get; }
        event Action OnJumpRadiusChanged;
    }

    public interface IPlayer
    {
        IUnitHealth UnitHealth { get; }
    }
}