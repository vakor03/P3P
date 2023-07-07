#region

using System;
using UnityEngine;

#endregion

namespace _Scripts.Units
{
    public class MeleeEnemy : EnemyBase, IDamageable
    {
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
                Destroy(gameObject);
            }
        }

        private Vector3 GetPlayerDirection()
        {
            var playerDirection = Player.Instance.transform.position - _transform.position;
            playerDirection.Normalize();

            return new Vector3(playerDirection.x, 0, playerDirection.z);
        }

        public void TakeDamage()
        {
            throw new NotImplementedException();
        }
    }
}