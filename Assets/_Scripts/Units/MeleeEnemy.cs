using System;
using UnityEngine;

namespace _Scripts.Units
{
    public class MeleeEnemy : EnemyBase
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

        private Vector3 GetPlayerDirection()
        {
            var playerDirection = Player.Instance.transform.position - _transform.position;
            playerDirection.Normalize();

            return new Vector3(playerDirection.x, 0, playerDirection.z);
        }
    }
}