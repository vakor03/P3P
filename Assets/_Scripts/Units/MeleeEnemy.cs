#region

using System;
using UnityEngine;

#endregion

namespace _Scripts.Units
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
            if (other.transform == Player.Instance.transform)
            {
                Player.Instance.TakeDamage();
                TakeDamage();
            }
        }

        public static event Action<Vector3> OnEnemyDead;

        private Vector3 GetPlayerDirection()
        {
            var playerDirection = Player.Instance.transform.position - _transform.position;
            playerDirection.Normalize();

            return new Vector3(playerDirection.x, 0, playerDirection.z);
        }

        public event Action OnDamageTaken;

        public void TakeDamage()
        {
            OnDamageTaken?.Invoke();
            OnEnemyDead?.Invoke(_transform.position);
            Destroy(gameObject);
        }
    }
}