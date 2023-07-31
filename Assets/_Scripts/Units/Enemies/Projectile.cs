using System;
using _Scripts.Units.Players;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public class Projectile : MonoBehaviour
    {
        private float _speed;
        private Vector3 _direction;
        public void SetMovement(float speed, Vector3 direction)
        {
            _speed = speed;
            _direction = direction;
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPlayer player))
            {
                player.UnitHealth.ReceiveDamage(1);
                DestroySelf();
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}