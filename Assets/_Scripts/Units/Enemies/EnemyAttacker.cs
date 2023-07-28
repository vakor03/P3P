using System.Collections.Generic;
using _Scripts.Helpers;
using MEC;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float delayBetweenAttacks;
        [SerializeField] private float rechargeTime;
        [SerializeField] private int projectilesCount;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PerformAttack();
            }
        }

        public void PerformAttack()
        {
            Coroutines.StartRoutineMEC(AttackRoutineMEC());
        }

        private bool _recharging = false;
        private bool _isAttacking = false;

        public bool CanAttack()
        {
            return !_recharging && !_isAttacking;
        }

        private IEnumerator<float> AttackRoutineMEC()
        {
            _isAttacking = true;
            
            float innerCircleRadius = 1f;
            for (int i = 0; i < projectilesCount; i++)
            {
                var spawnPosition = new Vector3(
                    innerCircleRadius * Mathf.Sin((2 * Mathf.PI / 10) * -i),
                    0,
                    innerCircleRadius * Mathf.Cos((2 * Mathf.PI / 10) * -i));

                SpawnProjectile(transform.position + spawnPosition, spawnPosition, projectileSpeed);
                yield return Timing.WaitForSeconds(delayBetweenAttacks);
            }

            _isAttacking = false;

            Coroutines.StartRoutineMEC(RechargingRoutineMEC());
        }

        private IEnumerator<float> RechargingRoutineMEC()
        {
            _recharging = true;
            yield return Timing.WaitForSeconds(rechargeTime);
            _recharging = false;
        }

        private Projectile SpawnProjectile(Vector3 position, Vector3 movementDirection, float speed)
        {
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

            projectile.SetMovement(speed, movementDirection);
            return projectile;
        }
    }
}