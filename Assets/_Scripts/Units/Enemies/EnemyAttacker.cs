#region

using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using MEC;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float delayBetweenAttacks;
        [SerializeField] private float rechargeTime;
        [SerializeField] private int projectilesCount;
        [SerializeField] private float preAttackDelay;

        private CoroutineHandle _attackRoutine;
        private CoroutineHandle _rechargingRoutine;
        
        private bool _isAttacking = false;
        private bool _recharging = false;
        
        public event Action OnAttackFinished;

        public void PerformAttack()
        {
            _attackRoutine = Coroutines.StartRoutineMEC(AttackRoutineMEC());
        }

        public void StopAttack()
        {
            if (_isAttacking)
            {
                Coroutines.StopRoutineMEC(_attackRoutine);
            }

            if (_recharging)
            {
                Coroutines.StopRoutineMEC(_rechargingRoutine);
            }
        }

        public bool CanAttack()
        {
            return !_recharging && !_isAttacking;
        }

        private IEnumerator<float> AttackRoutineMEC()
        {
            _isAttacking = true;
            
            yield return Timing.WaitForSeconds(preAttackDelay);

            float innerCircleRadius = 1f;
            for (int i = 0; i < projectilesCount; i++)
            {
                var spawnPosition = new Vector3(
                    innerCircleRadius * Mathf.Sin((2 * Mathf.PI / 10) * -i),
                    0,
                    innerCircleRadius * Mathf.Cos((2 * Mathf.PI / 10) * -i));

                var projectile =
                    SpawnProjectile(transform.position + spawnPosition,
                        spawnPosition,
                        projectileSpeed);

                _projectiles.Add(projectile);
                yield return Timing.WaitForSeconds(delayBetweenAttacks);
            }

            _isAttacking = false;
            OnAttackFinished?.Invoke();

            _rechargingRoutine = Coroutines.StartRoutineMEC(RechargingRoutineMEC());
        }

        private IEnumerator<float> RechargingRoutineMEC()
        {
            _recharging = true;
            yield return Timing.WaitForSeconds(rechargeTime);
            _recharging = false;
        }

        private List<Projectile> _projectiles = new();

        private Projectile SpawnProjectile(Vector3 position, Vector3 movementDirection, float speed)
        {
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

            projectile.SetMovement(speed, movementDirection);
            return projectile;
        }

        private void OnDestroy()
        {
            // foreach (var projectile in _projectiles)
            // {
            //     projectile.DestroySelf();
            // }
            StopAttack();
        }
    }
}