#region

using _Scripts.Animations;
using _Scripts.HealthSystems;
using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts.Units.Players
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : StaticInstance<Player>, IPlayer
    {
        [SerializeField] private float jumpRadiusModifier = 1f;
        [SerializeField] private float radiusOffset = 0.5f;
        [SerializeField] private Transform playerVisuals;

        private IAnimation _animation;
        private DamageReceiver _damageReceiver;


        private bool _invincible;
        private JumpRadiusController _jumpRadiusController;


        private PlayerAttacker _playerAttacker;
        private PlayerMover _playerMover;

        private Transform _transform;

        private IUnitHealth _unitHealth;
        public IPlayerMover PlayerMover => _playerMover;
        public JumpRadiusController JumpRadiusController => _jumpRadiusController;

        public IUnitHealth UnitHealth => _unitHealth;
        public IAnimation Animation => _animation;

        public float JumpDuration => _playerMover.JumpDuration;

        public DamageReceiver DamageReceiver => _damageReceiver;

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
}