#region

using System;
using _Scripts.HealthSystems;
using _Scripts.Systems;
using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public abstract class EnemyBase : UnitBase, IEnemy
    {
        public static event Action<Vector3> OnAnyEnemyDead;
        public IUnitHealth UnitHealth { get; protected set; }

        public virtual void Init()
        {
        }

        protected virtual void HandleDeath(Vector3 deathPosition)
        {
            OnAnyEnemyDead?.Invoke(deathPosition);
        }
        
        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPlayer player))
            {
                player.DamageReceiver.ReceiveDamage(1);
                UnitHealth.ReceiveDamage(1);
            }
        }
    }

    public interface IEnemy
    {
        IUnitHealth UnitHealth { get; }
        void SetStats(Stats stats);
        void Init();
    }
}