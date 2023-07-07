#region

using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : StaticInstance<Player>, IDamageable
    {
        private int _health = 3;
        private PlayerMover _playerMover;
        private Transform _transform;
        public float JumpRadius => _playerMover.JumpRadius;
        public float JumpDuration => _playerMover.JumpDuration;

        protected override void Awake()
        {
            base.Awake();

            _playerMover = GetComponent<PlayerMover>();
        }


        public void TakeDamage()
        {
            _health--;
        }
    }
}