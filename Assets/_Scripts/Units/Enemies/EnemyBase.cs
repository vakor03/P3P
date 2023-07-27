#region

using System;
using _Scripts.HealthSystems;
using _Scripts.Systems;
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
    }

    public interface IEnemy
    {
        IUnitHealth UnitHealth { get; }
        void SetStats(Stats stats);
        void Init();
    }
}