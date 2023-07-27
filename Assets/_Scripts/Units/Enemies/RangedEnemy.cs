#region

using _Scripts.HealthSystems;
using _Scripts.Units.Enemies.RangedEnemyStates;
using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public class RangedEnemy : EnemyBase
    {
        private IRangedEnemyState _currentState;
        public IUnitHealth UnitHealth { get; private set; }
        public float DistanceToPlayer => Vector3.Distance(transform.position, Player.Instance.transform.position);
        public Transform Transform => transform;

        private void Awake()
        {
            UnitHealth = new UnitHealth(Stats.health);
            SetDefaultState();
        }

        private void Update()
        {
            _currentState.Update(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPlayer player))
            {
                player.UnitHealth.ReceiveDamage(Stats.damage);
            }
        }

        public void SwitchState(IRangedEnemyState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }

            _currentState = newState;
            _currentState.Enter(this);
        }

        private void SetDefaultState()
        {
            SwitchState(new MovingTowardsState());
        }
    }
}