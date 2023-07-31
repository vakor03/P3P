#region

using System;
using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public class MeleeEnemy : EnemyBase, IDamageable
    {
        private bool _dead;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            var playerDirection = GetPlayerDirection();

            _transform.position += Time.deltaTime * Stats.speed * playerDirection;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageReceiver damageReceiver) && damageReceiver.IsPlayer)
            {
                damageReceiver.ReceiveDamage(1);
                ReceiveDamage();
            }
        }
        
        private Vector3 GetPlayerDirection()
        {
            var playerDirection = Player.Instance.transform.position - _transform.position;
            playerDirection.Normalize();

            return new Vector3(playerDirection.x, 0, playerDirection.z);
        }

        public event Action OnDamageTaken;

        public void ReceiveDamage()
        {
            OnDamageTaken?.Invoke();
            HandleDeath(_transform.position);
            Destroy(gameObject);
        }
    }
}