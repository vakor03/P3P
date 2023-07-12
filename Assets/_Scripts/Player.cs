#region

using System;
using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : StaticInstance<Player>, IDamageable
    {
        private const int COLLIDER_BUFFER_COUNT = 10;
        
        [SerializeField] private SphereCollider attackCollider;
        [SerializeField] private LayerMask enemiesLayerMask;
        
        private readonly Collider[] _colliderBuffer = new Collider[COLLIDER_BUFFER_COUNT];
        private int _health = 3;

        private bool _invincible;
        private PlayerMover _playerMover;
        private Transform _transform;
        public float JumpRadius => _playerMover.JumpRadius;
        public float JumpDuration => _playerMover.JumpDuration;


        protected override void Awake()
        {
            base.Awake();

            _playerMover = GetComponent<PlayerMover>();
        }

        private void Start()
        {
            _playerMover.OnJumpStarted += PlayerMoverOnJumpStarted;
            _playerMover.OnJumpFinished += PlayerMoverOnJumpFinished;
        }
        public event Action OnDamageTaken;

        private void PlayerMoverOnJumpStarted()
        {
            _invincible = true;
        }
 
        private void PlayerMoverOnJumpFinished()
        {
            _invincible = false;

            Vector3 position = attackCollider.transform.position;
            float radius = attackCollider.radius;
            int count = Physics.OverlapSphereNonAlloc(position, radius, _colliderBuffer, enemiesLayerMask);
            for (int i = 0; i < count; i++)
            {
                if (_colliderBuffer[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage();
                }
            }
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